using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ekUtilities
{
    public class FileRepository<T> : IRepository<T> where T: BaseEntity
    {
        //log4net.ILog log4 = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string file
        {
            get { return System.Web.HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}.js", typeof(T).Name)); }
        }

        private void Persist(List<T> data)
        {
            var serializer = new JavaScriptSerializer();
            StreamWriter sw = null;
            FileStream fs = null;
            try
            {
                if (!File.Exists(file))
                {
                    fs = File.Create(file);
                }
                else
                    fs = File.Open(file, FileMode.Truncate);
                sw = new StreamWriter(fs);
                sw.Write(serializer.Serialize(data));
                sw.Flush();
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }

        public IQueryable<T> Get()
        {
            var serializer = new JavaScriptSerializer();
            if (!File.Exists(file))
                Persist(new List<T>());
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(file);
                //log4.Warn("reading from file");
                return serializer.Deserialize<List<T>>(sr.ReadToEnd()).AsQueryable();
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }
        }

        public T Save(T t)
        {
            var items = Get().ToList();
            T item = null;
            if (t.Id != 0)
            {
                item = items.SingleOrDefault(i => i.Id == t.Id);
                items.Remove(item);
            }
            else
            {
                t.Id = items.Count == 0 ? 1 : items.Max(i => i.Id) + 1;
            }
            items.Add(t);
            Persist(items);
            return Get().SingleOrDefault(i => i.Id == t.Id);

        }

        public void Delete(T t)
        {
            var items = Get().ToList();
            var item  = items.SingleOrDefault(i => i.Id == t.Id);
            items.Remove(item);
            Persist(items);
        }
    }
}