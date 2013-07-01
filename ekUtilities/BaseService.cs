using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ekUtilities
{
    public  class BaseService<T> where T: BaseEntity
    {
        private IRepository<T> DAL = null;
        private ICacheProvider store = null;
        private ILogProvider log = null;

        public string Key
        {
            get { return typeof (T).Name; }
        }


        public BaseService(IRepository<T> DAL, ICacheProvider store, ILogProvider log)
        {
            this.DAL = DAL;
            this.store = store;
            this.log = log;
        }

        public ICollection<T> Get()
        {

            if (store.Get<List<T>>(Key) != null)
                return store.Get<List<T>>(Key);
            var data = DAL.Get().ToList();
            if (this.log != null)
                log.Write(String.Format("Getting data for {0} class", typeof (T)), log4net.Core.Level.Info);
            store.Set(Key, data);
            return data;
        }

        public T Get(int? id)
        {
            if (id.HasValue)
                return Get().SingleOrDefault(w => w.Id == id);
            return default(T);
        }

        public T Set(T t)
        {
            if (t.IsNew())
                t.DateCreated = DateTime.Now;
            else
                t.DateModified = DateTime.Now;
            T item = DAL.Save(t);
            store.Remove(Key);
            return item;
        }

        public void Clear()
        {
            foreach (var item in Get())
                Delete(item);
        }
        public void Delete(T t)
        {
            DAL.Delete(t);
            store.Remove(Key);
        }
    }
}