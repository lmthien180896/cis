using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CIS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Apply Job",
               url: "form-tuyen-dung/{id}",
               defaults: new { controller = "Job", action = "ApplyView", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Recruit",
               url: "tuyen-dung",
               defaults: new { controller = "Job", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Feedback",
               url: "gop-y",
               defaults: new { controller = "Contact", action = "Feedback", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "View Notification",
               url: "thong-bao/{alias}-{id}",
               defaults: new { controller = "Notification", action = "ViewNotification", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "List Notification",
               url: "thong-bao",
               defaults: new { controller = "Notification", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "View News",
               url: "tin-tuc/{alias}-{id}",
               defaults: new { controller = "News", action = "ViewNews", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "List News",
               url: "tin-tuc",
               defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Send Request Form",
               url: "gui-yeu-cau",
               defaults: new { controller = "Request", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Design Service",
               url: "dich-vu-thiet-ke-do-hoa",
               defaults: new { controller = "Service", action = "Design", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Web Service",
               url: "dich-vu-web",
               defaults: new { controller = "Service", action = "Web", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );

            routes.MapRoute(
               name: "System Service",
               url: "dich-vu-he-thong",
               defaults: new { controller = "Service", action = "System", id = UrlParameter.Optional },
               namespaces: new[] { "CIS.Web.Controllers" }
            );
            routes.MapRoute(
                name: "About us",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional },
                namespaces: new[] { "CIS.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "CIS.Web.Controllers" }
            );
        }
    }
}
