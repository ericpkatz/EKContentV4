using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ekUtilities.membership
{
    public class RoleService
    {
        private BaseService<Role> _svc;

        public RoleService(IRepository<Role> repo, ICacheProvider cacheProvider, ILogProvider log)
        {
            _svc = new BaseService<Role>(repo, cacheProvider, log);
        }
        public List<Role> Roles
        {
            get { return _svc.Get().ToList(); }
        }

        public bool RoleExists(string role)
        {
            return Roles.Any(u => u.Name.ToLower() ==role.ToLower());
        }

        public Role GetRole(int id)
        {
            return Roles.SingleOrDefault(uf => uf.Id == id);
        }

        public Role GetRole(string role)
        {
            return Roles.SingleOrDefault(u => u.Name.ToLower() == role.ToLower());
        }

        public Role Save(Role role)
        {
            return _svc.Set(role);
        }

        public Role Delete(int id)
        {
            var user = GetRole(id);
            _svc.Delete(user);
            return user;
        }


    }
}