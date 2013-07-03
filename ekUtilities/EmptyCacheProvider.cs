namespace ekUtilities
{
    public class EmptyCacheProvider : ICacheProvider
    {
        public void Set(string key, object value)
        {
            
        }

        public T Get<T>(string key)
        {
            return default(T);
        }

        public void Remove(string key)
        {
            
        }
    }
}
