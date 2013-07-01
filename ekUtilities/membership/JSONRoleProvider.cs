using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace ekUtilities.membership
{
    public class JSONRoleProvider : RoleProvider
    {
        private RoleService _svc = new RoleService(new FileRepository<Role>(), new WebCacheProvider(), null);
        private UserRoleService _userRoleService = new UserRoleService(new FileRepository<UserRole>(), new WebCacheProvider(), null);
        private UserService _userService = new UserService(new FileRepository<User>(), new WebCacheProvider(), null);
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var roleIds = _svc.Roles.Where(r => roleNames.Contains(r.Name)).Select(r => r.Id).ToArray();
            foreach (var username in usernames)
            {
                var user = _userService.GetUser(username);
                foreach (var role in roleIds)
                {
                    _userRoleService.Save(new UserRole {RoleId = role, UserId = user.Id});
                }
            }
        }

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

        public override void CreateRole(string roleName)
        {
            var role = new Role {Name = roleName};
            _svc.Save(role);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return _svc.Roles.Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _userService.GetUser(username);
            var roleIds = _userRoleService.GetUserRolesForUser(user.Id).Select(r=>r.RoleId);
            return _svc.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.Name).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = _svc.GetRole(roleName);
            var userIds = _userRoleService.UserRoles.Where(ur => ur.RoleId == role.Id).Select(ur => ur.UserId);
            return _userService.Users.Where(u => userIds.Contains(u.Id)).Select(u => u.Username).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var role = _svc.GetRole(roleName);
            var user = _userService.GetUser(username);
            return _userRoleService.GetUserRolesForUser(user.Id).Any(ur => ur.RoleId == role.Id);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var roleIds = _svc.Roles.Where(r => roleNames.Contains(r.Name)).Select(r => r.Id).ToArray();
            foreach (var username in usernames)
            {
                var user = _userService.GetUser(username);
                foreach (var role in roleIds)
                {
                    var userRoles = _userRoleService.GetUserRolesForUser(user.Id);
                    if(userRoles.Any(ur=>ur.RoleId == role))
                    {
                        var userRole = userRoles.SingleOrDefault(r => r.RoleId == role);
                        _userRoleService.Delete(userRole.Id);
                    }
                }
            }
        }

        public override bool RoleExists(string roleName)
        {
            return _svc.RoleExists(roleName);
        }
    }
}
