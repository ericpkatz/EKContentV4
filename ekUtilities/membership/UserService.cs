using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ekUtilities;

namespace ekUtilities.membership
{
    public class UserService
    {
        private BaseService<User> _svc;

        public UserService(IRepository<User> repo, ICacheProvider cacheProvider, ILogProvider log)
        {
            _svc = new BaseService<User>(repo, cacheProvider, log);
        }
        public List<User> Users
        {
            get { return _svc.Get().ToList(); }
        }

        public bool UserExists(string userName)
        {
            return Users.Any(u => u.Username.ToLower() == userName.ToLower());
        }

        public User GetUser(int id)
        {
            return Users.SingleOrDefault(uf => uf.Id == id);
        }

        public User GetUser(string username)
        {
            return Users.SingleOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public User Save(User userFile)
        {
            return _svc.Set(userFile);
        }

        public User Delete(int id)
        {
            var user = GetUser(id);
            _svc.Delete(user);
            return user;
        }

        public User Authenticate(User user)
        {
            return
                Users.SingleOrDefault(
                    u =>
                    u.Username.ToLower() == user.Username.ToLower() &&
                    user.Password.ToLower() == user.Password.ToLower());
        }

    }
}