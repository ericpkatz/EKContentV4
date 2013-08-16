using EKContent.bus.Abstract;
using EKContent.bus.Entities;
using EKContent.bus.Entitities;
using ekUtilities;

namespace EKContent.bus.Concrete
{
    public class EKProvider : IEKProvider
    {
        public IBaseService<PageNavigation> PageNavigationProvider
        {
            get { return new BaseService<PageNavigation>(new FileRepository<PageNavigation>(), new WebCacheProvider(), null); }
        }

        public IEkDataProvider DataProvider
        {
            get { return new EkDataProvider(); }
        }

        public IBaseService<Site> SiteProvider
        {
            get { return new BaseService<Site>(new FileRepository<Site>(), new WebCacheProvider(), null); }
        }

        public IBaseService<Feed> FeedProvider
        {
            get { return new FeedProvider(); }
        }

        public IBaseService<Image> ImageProvider
        {
            get { return new BaseService<Image>(new FileRepository<Image>(), new WebCacheProvider(), null); }
        }


        //public IEKRoleProvider RoleProvider
        //{
        //    get { return new EKRoleProvider(); }
        //}


        public IBaseService<TwitterKeys> TwitterKeysProvider
        {
            get { return new BaseService<TwitterKeys>(new FileRepository<TwitterKeys>(), new WebCacheProvider(), null); }
        }


        public IBaseService<StyleSettings> StyleSettingsProvider
        {
            get { return new BaseService<StyleSettings>(new FileRepository<StyleSettings>(), new WebCacheProvider(), null); }
        }


        public IBaseService<Color> ColorProvider
        {
            get { return new BaseService<Color>(new FileRepository<Color>(), new WebCacheProvider(), null); }
        }

        public IBaseService<EKFile> FileProvider
        {
            get { return new BaseService<EKFile>(new FileRepository<EKFile>(), new WebCacheProvider(), null); }
        }



        public Newtonsoft.Json.Linq.JArray Tweets(TwitterKeys keys)
        {
            return new TweetProvider().Tweets(keys);
        }

        public EKContent.bus.Abstract.ICacheProvider CacheProvider
        {
            get { return new CacheProvider(); }
        }
    }
}