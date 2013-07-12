using EKContent.bus.Entities;
using EKContent.bus.Entitities;
using ekUtilities;
using Newtonsoft.Json.Linq;

namespace EKContent.bus.Abstract
{
    public interface IEKProvider
    {
        ICacheProvider CacheProvider { get;  }
        BaseService<PageNavigation> PageNavigationProvider { get; }
        IEkDataProvider DataProvider{ get;}
        BaseService<Site> SiteProvider { get; }
        BaseService<Image> ImageProvider { get; }
        IEKRoleProvider RoleProvider { get; }
        BaseService<TwitterKeys> TwitterKeysProvider { get; }
        BaseService<StyleSettings> StyleSettingsProvider { get;  }
        BaseService<Color> ColorProvider { get;  }
        BaseService<EKFile> FileProvider { get; }
        BaseService<Feed> FeedProvider { get; }
        JArray Tweets(TwitterKeys keys);
    }
}
