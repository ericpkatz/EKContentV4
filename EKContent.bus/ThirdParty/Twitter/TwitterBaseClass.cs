using EKContent.bus.Concrete;
using EKContent.bus.Entities;
using EKContent.bus.Services;

namespace EKContent.bus.ThirdParty.Twitter
{
    public class TwitterBaseClass
    {
        protected PageService Service()
        {
            //return (PageService)NinjectControllerFactory.NinjectKernel.GetService(typeof(PageService));
            return new PageService(new EKProvider());
        }

        protected TwitterKeys Keys()
        {
            return Service().GetTwitterKeys();
        }


        protected string ConsumerKey
        {
            get { return Keys().ApplicationKey; }
        }

        protected string ConsumerSecret
        {
            get { return Keys().ApplicationSecret; }
        }
    }
}