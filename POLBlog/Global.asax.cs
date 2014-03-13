using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using POLBlog.Domain.Entities;
using POLBlog.Infrastructure;
using POLBlog.Models;

namespace POLBlog
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{page}", // URL with parameters
                new { controller = "Posts", action = "List", page=1} // Parameter defaults
            );

               routes.MapRoute(
                "AdminPost", // Route name
                "{controller}/{action}/{category}", // URL with parameters
                new { controller = "Admin", action = "ShowPosts", category = "PostsAdmin" } // Parameter defaults
            );

               routes.MapRoute(
                "AdminArt", // Route name
                "{controller}/{action}/{category}", // URL with parameters
                new { controller = "Admin", action = "ShowArts", category = "ArtAdmin" } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Mapper.CreateMap<Post, PostViewModel>().ForMember(pvm => pvm.ArticleBody,
                                                              m => m.MapFrom(s => s.ArticleBody.Substring(0, 300)));

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}