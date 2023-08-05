using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace SchoolTours
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Off;
            //settings.AutoRedirectMode = RedirectMode.Permanent; -- change for call jquery to c# function 
            //---------- when you redirect to one page to another page make sure must remove the ".aspx" extenstion 
            routes.EnableFriendlyUrls(settings);
        }
    }
}
