using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ekUtilities.membership
{
    public class UserRoleService
    {
        private BaseService<UserRole> _svc;

        public UserRoleService(IRepository<UserRole> repo, ICacheProvider cacheProvider, ILogProvider log)
        {
            _svc = new BaseService<UserRole>(repo, cacheProvider, log);
        }
        public List<UserRole> UserRoles
        {
            get { return _svc.Get().ToList(); }
        }


        public UserRole GetUserRole(int id)
        {
            return UserRoles.SingleOrDefault(uf => uf.Id == id);
        }

        public List<UserRole> GetUserRolesForUser(int userId)
        {
            return UserRoles.Where(u => u.UserId == userId).ToList();
        }

        public UserRole Save(UserRole role)
        {
            return _svc.Set(role);
        }

        public UserRole Delete(int id)
        {
            var user = GetUserRole(id);
            _svc.Delete(user);
            return user;
        }


    }
}