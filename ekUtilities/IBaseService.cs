using System.Collections.Generic;

namespace ekUtilities
{
    public interface IBaseService<T> where T : BaseEntity
    {
        string Key { get; }
        ICollection<T> Get();
        T Get(int? id);
        T Set(T t);
        void Clear();
        void Delete(T t);
    }
}