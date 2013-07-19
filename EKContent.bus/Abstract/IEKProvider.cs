using EKContent.bus.Entities;
using EKContent.bus.Entitities;
using ekUtilities;
using Newtonsoft.Json.Linq;

namespace EKContent.bus.Abstract
{
    public interface IEKProvider
    {
        ICacheProvider CacheProvider { get;  }
        IBaseService<PageNavigation> PageNavigationProvider { get; }
        IEkDataProvider DataProvider{ get;}
        IBaseService<Site> SiteProvider { get; }
        IBaseService<Image> ImageProvider { get; }
        IEKRoleProvider RoleProvider { get; }
        IBaseService<TwitterKeys> TwitterKeysProvider { get; }
        IBaseService<StyleSettings> StyleSettingsProvider { get; }
        IBaseService<Color> ColorProvider { get; }
        IBaseService<EKFile> FileProvider { get; }
        IBaseService<Feed> FeedProvider { get; }
        JArray Tweets(TwitterKeys keys);
    }
}
