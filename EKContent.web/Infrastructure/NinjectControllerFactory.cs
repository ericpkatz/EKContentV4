﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EKContent.bus.Abstract;
using EKContent.bus.Concrete;
using Ninject;

namespace EKContent.web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        public static IKernel NinjectKernel
        {
            get { return _kernel; }
        }

        static Ninject.IKernel _kernel = null;
        public NinjectControllerFactory()
        {
            _kernel = new Ninject.StandardKernel();
            //_kernel.Bind<INavigationProvider>().To<NavigationProvider>();
            //_kernel.Bind<IEkDataProvider>().To<EkDataProvider>();
            _kernel.Bind<IEKProvider>().To<EKProvider>();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)_kernel.GetService(controllerType);
        }

    }
}