using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.web.Models.Database.Abstract;
using EKContent.web.Models.Entities;
using ekUtilities;

namespace EKContent.web.Models.Database.Concrete
{
    public class EKProvider : IEKProvider
    {
        public BaseService<PageNavigation> PageNavigationProvider
        {
            get { return new BaseService<PageNavigation>(new FileRepository<PageNavigation>(), new WebCacheProvider(), null); }
        }

        public IEkDataProvider DataProvider
        {
            get { return new EkDataProvider(); }
        }

        public BaseService<Site> SiteProvider
        {
            get { return new BaseService<Site>(new FileRepository<Site>(), new WebCacheProvider(), null); }
        }

        public BaseService<Image> ImageProvider
        {
            get { return new BaseService<Image>(new FileRepository<Image>(), new WebCacheProvider(), null); }
        }


        public IEKRoleProvider RoleProvider
        {
            get { return new EKRoleProvider(); }
        }


        public BaseService<TwitterKeys> TwitterKeysProvider
        {
            get { return new BaseService<TwitterKeys>(new FileRepository<TwitterKeys>(), new WebCacheProvider(), null); }
        }


        public BaseService<StyleSettings> StyleSettingsProvider
        {
            get { return new BaseService<StyleSettings>(new FileRepository<StyleSettings>(), new WebCacheProvider(), null); }
        }


        public BaseService<Color> ColorProvider
        {
            get { return new BaseService<Color>(new FileRepository<Color>(), new WebCacheProvider(), null); }
        }

        public BaseService<EKFile> FileProvider
        {
            get { return new BaseService<EKFile>(new FileRepository<EKFile>(), new WebCacheProvider(), null); }
        }



        public Newtonsoft.Json.Linq.JArray Tweets(TwitterKeys keys)
        {
            return new TweetProvider().Tweets(keys);
        }

        public Abstract.ICacheProvider CacheProvider
        {
            get { return new CacheProvider(); }
        }
    }
}