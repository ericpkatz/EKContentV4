using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ekUtilities
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get();
        T Save(T t);
        void Delete(T t);

    }
}