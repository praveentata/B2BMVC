using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace B2B
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Login",
               url: "login",
               defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Register",
               url: "register",
               defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Approve Invoice",
               url: "approve/{id}",
               defaults: new { controller = "Invoice", action = "ApproveInvoice", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Send PO",
               url: "send/",
               defaults: new { controller = "PO", action = "sendPO", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "PO Details",
               url: "po-detail/{search}",
               defaults: new { controller = "PO", action = "Details", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "All POES",
               url: "all-poes/{id}",
               defaults: new { controller = "PO", action = "Invoice", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "PO",
               url: "po",
               defaults: new { controller = "PO", action = "po", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Shopping Cart",
               url: "cart/{id}",
               defaults: new { controller = "ShoppingCart", action = "cart", id = UrlParameter.Optional });


            routes.MapRoute(
               name: "Home",
               url: "products",
               defaults: new { controller = "Products", action = "products", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}