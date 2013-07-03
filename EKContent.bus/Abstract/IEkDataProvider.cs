using System.Collections.Generic;
using EKContent.bus.Entities;

namespace EKContent.bus.Abstract
{
    public interface IEkDataProvider
    {
        List<Module> Get(int id);
        void Save(Page page);
        void Delete(int id);
    }
}