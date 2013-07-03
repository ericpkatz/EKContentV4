using System.Collections.Generic;
using EKContent.bus.Entities;

namespace EKContent.bus.Abstract
{
    public interface IEKRoleProvider
    {
        IEnumerable<EKRole> Get();
        void Set(List<EKRole> roles);
        void Delete(int id);
        void Save(EKRole role);
    }
}