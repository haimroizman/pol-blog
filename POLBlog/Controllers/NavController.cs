using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POLBlog.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        public PartialViewResult Menu(string category = "Posts")
        {
            ViewBag.SelectedIssue = category;
            IEnumerable<string> blogIssues = new[] {"Posts", "Art", "Photography"};
            return PartialView(blogIssues);
        }

        public PartialViewResult AdminMenu(string category)
        {
            ViewBag.SelectedIssue = category ?? TempData["selectedIsuue"];
                
            IEnumerable<string> blogIssues = new[] { "PostsAdmin", "ArtAdmin" };
            return PartialView("Menu",blogIssues);
        }

    }
}
