using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EKContent.web.Models.Entities;
using ekUtilities;
using Newtonsoft.Json.Linq;

namespace EKContent.web.Models.Database.Abstract
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
        //IColorProvider ColorProvider { get; }
        BaseService<Color> ColorProvider { get;  }
        BaseService<EKFile> FileProvider { get; }
        JArray Tweets(TwitterKeys keys);
    }
}
