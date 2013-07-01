using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using EKContent.web.Models.Database.Abstract;
using EKContent.web.Models.Entities;
using EKContent.web.Models.Styles;
using EKContent.web.Models.ViewModels;
using EKContent.web.ThirdParty.Twitter;
using Newtonsoft.Json.Linq;

namespace EKContent.web.Models.Services
{
    public class PageService
    {
        private IEkDataProvider _dataProvider = null;
        private IEKRoleProvider _roleProvider = null;
        private IEKProvider _dal = null;



        public StyleSettings GetStyleSettings()
        {
            if (_dal.StyleSettingsProvider.Get().Count == 0)
                SetStyleSettings(new StyleSettings
                {
                });
            return _dal.StyleSettingsProvider.Get().First();
        }

        public void SetStyleSettings(StyleSettings settings)
        {
            _dal.StyleSettingsProvider.Clear();
            _dal.StyleSettingsProvider.Set(settings);
        }

        public Site GetSite()
        {
            if(_dal.SiteProvider.Get().Count == 0)
                SetSite(new Site
                               {
                                   Title = "New Site",
                                   Email = "Test@Test.com"
                               });
            return _dal.SiteProvider.Get().First();
        }

        public void SetSite(Site site)
        {
            _dal.SiteProvider.Clear();
            _dal.SiteProvider.Set(site);
        }

        public StylesProvider StylesProvider()
        {
            return new StylesProvider(this);
        }

        //public PageService(INavigationProvider navigationProvider, IEkDataProvider dataProvider, IEkSiteDataProvider siteProvider, IImageDataProvider imageProvider)
        //{
        //    _navigationProvider = navigationProvider;
        //    _dataProvider = dataProvider;
        //    _siteProvider = siteProvider;
        //    _imageProvider = imageProvider;
        //}

        public IEKProvider Dal
        {
            get
            {
                return _dal;
            }
        }

        public PageService(IEKProvider provider)
        {
            _dal = provider;
            _dataProvider = provider.DataProvider;
            _roleProvider = provider.RoleProvider;

        }

        public Page GetHomePage()
        {
            if (_dal.PageNavigationProvider.Get().Count == 0)
                _dal.PageNavigationProvider.Set(new PageNavigation
                {
                    Title = "Home"
                });
            var page = _dal.PageNavigationProvider.Get().Where(p => p.IsHomePage()).Single();
            return GetPage(page.Id);
        }

        public List<PageNavigation> GetNavigation()
        {
            return _dal.PageNavigationProvider.Get().OrderBy(p => p.Priority).ToList();
        }

        public void SavePageNavigation(PageNavigation pageNavigation)
        {
            _dal.PageNavigationProvider.Set(pageNavigation);
        }

        public Page GetPage(int id)
        {
            var page = new Page {PageNavigation = _dal.PageNavigationProvider.Get().Where(p => p.Id == id).Single()};
            
            page.Modules = _dataProvider.Get(page.PageNavigation.Id);
            foreach(var module in page.Modules)
                foreach (var item in module.Content)
                {
                    if (item.ImageId != 0)
                    {
                        var image = _dal.ImageProvider.Get().SingleOrDefault(i => i.Id == item.ImageId);
                        item.Image = image;
                    }
                    if (item.FileId != 0)
                    {
                        var file = this.Dal.FileProvider.Get().SingleOrDefault(i => i.Id == item.FileId);
                        item.File = file;
                    }
                }
            return page;
        }

        public void DeletePage(int id)
        {
            _dataProvider.Delete(id);
        }

        public void SavePage(Page page)
        {
            foreach (Module m in page.Modules)
                m.Content = m.Content.OrderBy(c => c.Priority).ThenByDescending(c => c.DateCreated).ToList();
            _dataProvider.Save(page);
        }

        public Page MovePageToChildPage(int pageId)
        {
            var pages = GetNavigation();
            var page = new Page { PageNavigation = pages.Single(p => p.Id == pageId) };
            var modules = GetPage(pageId).Modules;
            foreach (Module m in modules)
                m.Content = m.Content.OrderBy(c => c.Priority).ThenByDescending(c => c.DateCreated).ToList();
            var newPage = page.Clone();
            newPage.Modules = modules;
            page.Modules = new List<Module>();
            //page.Title = "Change Title";

            newPage.PageNavigation.Id = pages.Max(p => p.Id) + 1;
            newPage.PageNavigation.Title = modules.SelectMany(m=>m.Content).First().Title;
            pages.Add(newPage.PageNavigation);
            _dataProvider.Save(newPage);
            _dataProvider.Save(page);
            SavePageNavigation(newPage.PageNavigation);
            SavePageNavigation(page.PageNavigation);
            return newPage;
        }

        //images
        public List<Image> GetImages()
        {
            return _dal.ImageProvider.Get().OrderByDescending(i=>i.DateCreated).ToList();
        }

        public string GetImage(int id)
        {
            var img = _dal.ImageProvider.Get().SingleOrDefault(i => i.Id == id);
            if (img == null)
                return String.Format("[IMG]{0}", id);
            else
                return img.FileName;
        }

        public void SaveImage(Image image)
        {
            _dal.ImageProvider.Set(image);
        }

        public string RenderImages(string content)
        {
            if (content == null)
                return String.Empty;
            var regex = new Regex(@"\[IMG(?<image_number>[0-9]+)\]");
            return regex.Replace(content, m => String.Format("<img src=\"user_images/{0}\"/>", GetImage(int.Parse(m.Groups["image_number"].Value))));

        }
        public void DeleteImage(int id)
        {
            var img = _dal.ImageProvider.Get().SingleOrDefault(i => i.Id == id);
            _dal.ImageProvider.Delete(img);
        }

        public List<EKRole> GetRoles()
        {
            return _roleProvider.Get().ToList();
        }


        public void SendMessage(Message message)
        {
            var site = this.GetSite();
            MailMessage msgeme = new MailMessage(message.Email, site.SmtpSiteEmail, String.Format("Message From {0}", message.Name), message.Body);
            SmtpClient smtpclient = new SmtpClient(site.SmtpServer);
            smtpclient.EnableSsl = false;
            smtpclient.Credentials = new NetworkCredential(site.SmtpSiteEmail, site.SmtpSiteEmailPassword);
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpclient.Send(msgeme);

        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            var user = Membership.GetUser();
            user.ChangePassword(oldPassword, newPassword);
        }

        public void SaveTwitterKeys(TwitterKeys keys)
        {
            _dal.TwitterKeysProvider.Clear();
            _dal.TwitterKeysProvider.Set(keys);
        }

        public TwitterKeys GetTwitterKeys()
        {
            if (_dal.TwitterKeysProvider.Get().Count == 0)
                SaveTwitterKeys(new TwitterKeys
                {
                });
            return _dal.TwitterKeysProvider.Get().First();
        }

        public JArray GetTweets()
        {
            return _dal.Tweets(GetTwitterKeys());
        }

        public bool ShowTwitterFeed()
        {
            return GetTwitterKeys().Configured && GetTwitterKeys().Configured;
        }

        public string Encrypt(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(input);
            data = x.ComputeHash(data);
            String md5Hash = System.Text.Encoding.ASCII.GetString(data);
            return md5Hash;
        }

        public string BootstrapStyleSheet()
        {
            return new StylesProvider(this).GetStyleSheet("bootstrap.min");
        }

        public string BootstrapResponsiveStyleSheet()
        {
            return new StylesProvider(this).GetStyleSheet("bootstrap-responsive.min");
        }

        private ServiceProviderDescription TwitterServiceDescription()
        {

            return new ServiceProviderDescription

            {
                AccessTokenEndpoint = new MessageReceivingEndpoint("https://api.twitter.com/oauth/access_token", HttpDeliveryMethods.PostRequest),
                RequestTokenEndpoint = new MessageReceivingEndpoint("https://api.twitter.com/oauth/request_token", HttpDeliveryMethods.PostRequest),
                UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://api.twitter.com/oauth/authorize", HttpDeliveryMethods.PostRequest),
                TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
                ProtocolVersion = ProtocolVersion.V10a
            };
        }

        public WebConsumer TwitterConsumer()
        {
            return new DotNetOpenAuth.OAuth.WebConsumer(TwitterServiceDescription(), new ShortTermTokenProvider());
        }
    }
}