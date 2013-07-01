using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ekUtilities;

namespace ekUtilities.membership
{
    public class JSONMembershipProvider : MembershipProvider
    {
        private UserService _svc = new UserService(new FileRepository<User>(), new WebCacheProvider(), null);
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _svc.Users.Single(u => u.Username.ToLower() == username.ToLower());
            user.Password = newPassword;
            _svc.Save(user);
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            //throw new NotImplementedException();
            var user = new User {Username = username, Password = password};
            user = _svc.Save(user);
            status = MembershipCreateStatus.Success;
            return new MembershipUser(this.GetType().Name, user.Username, user.Id, null, null, null, true, false,
                                      DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _svc.GetUser(username);
            if (user == null)
                return null;
            return GetUser(user.Id, false);
        }


        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey == null)
                return null;
            var user = _svc.GetUser((int) providerUserKey);
            if (user != null)
                return new MembershipUser(this.GetType().Name, user.Username, providerUserKey, null, null, null, true, false,
                                          DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
           
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            return
                _svc.Users.Any(
                    u => u.Username.ToLower() == username.ToLower() && password.ToLower() == u.Password.ToLower());
        }
    }
}