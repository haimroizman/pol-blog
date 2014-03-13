using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using POLBlog.Domain.Abstract;
using POLBlog.Domain.Entities;
using POLBlog.Domain.Extensions;
using POLBlog.Models;

//TODO:show posts in descended order-Done
//TODO:Add CreateDate to DB-Done
//TODO:PageSize
//TODO:Cash(the attribute chapter)
//TODO:Login
//TODO:Rest(chapter 14)
//TODO:AJAX(page size,Delete in admin,comments, move to page....)

namespace POLBlog.Controllers
{
    public class PostsController : Controller
    {
        private IPostsRepository postsRepository;
        private IArtsRepository artsRepository;
        public int PageSize = 4;
        public int ArtPageSize = 12;

        public PostsController(IPostsRepository repository, IArtsRepository aRepository)
        {
            postsRepository = repository;
            artsRepository = aRepository;
        }

        public ActionResult List(int page = 1)
        {
            Post[] posts = HttpRuntime.Cache.GetOrStore("Posts", () => postsRepository.Posts.ToArray());

            IEnumerable<PostViewModel> postsViewModel =
                Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts.Cast<Post>().AsEnumerable());
            
            PostsListsViewModel viewModel =
                new PostsListsViewModel
                    {
                        Posts =
                            postsViewModel.OrderByDescending(p => p.PostID).Skip((page - 1)*PageSize).
                            Take(PageSize),
                        PagingInfo = new PagingInfo
                                         {
                                             CurrentPage = page,
                                             ItemsPerPage = PageSize,
                                             TotalItems = postsViewModel.Count()
                                         },
                                         DrpPageSize=new List<SelectListItem>()

                    };
            foreach (string dDLI in ConfigurationManager.AppSettings["PostPageSize"].Split(','))
            {
                
                viewModel.DrpPageSize.Add(new SelectListItem{Text = dDLI.Split(':')[0],Value = dDLI.Split(':')[1]});
            }

            foreach (var imageP in Directory.EnumerateFiles(Server.MapPath("~/Content/PostImages"))
                .Select(fn => "~/Content/PostImages/" + Path.GetFileName(fn)).ToList())
            {
                ViewData[imageP.Split('/')[3].Split('.')[0]] = imageP;
            }

            return View(viewModel);
        }

        public ActionResult ArtList(int page = 1)
        {
            Art[] arts = HttpRuntime.Cache.GetOrStore("Arts", () => artsRepository.Arts.ToArray());

            ArtListsViewModel viewModel = new ArtListsViewModel
                                              {
                                                  Arts =
                                                      arts.OrderByDescending(a => a.ArtID).Skip((page - 1)*ArtPageSize).Take(
                                                          ArtPageSize),
                                                  PagingInfo = new PagingInfo
                                                                   {
                                                                       CurrentPage = page,
                                                                       ItemsPerPage = ArtPageSize,
                                                                       TotalItems = arts.Count()
                                                                   }
                                              };

            foreach (
                var imageA in
                    Directory.EnumerateFiles(Server.MapPath("~/Content/ArtImages")).Select(
                        fn => "~/Content/ArtImages/" + Path.GetFileName(fn)).ToList())
            {
                ViewData[imageA.Split('/')[3].Split('.')[0] + "Art"] = imageA;
            }



           return View(viewModel);
        }



        public ViewResult DisplayFullArticle(int PostID, string returnUrl)
        {
            Post thePostFullArticle = HttpRuntime.Cache.GetOrStore("Post" + PostID,
                                                                   () =>
                                                                   postsRepository.Posts.First(x => x.PostID == PostID));
            return View(new PostFullArticleViewModel {Post = thePostFullArticle, ReturnUrl = returnUrl});
        }

        public ViewResult DisplayArtImage(int artId, string returnUrl)
        {
            Art artImage = HttpRuntime.Cache.GetOrStore("Arts" + artId,
                                                        () => artsRepository.Arts.First(a => a.ArtID == artId));
            //ViewData["displayImage"] =
            //    Directory.EnumerateFiles(Server.MapPath("~/Content/ArtImages")).FirstOrDefault(
            //        fn => Path.GetFileName(fn).Split('.')[0] == artId.ToString());

            foreach (
                var imageA in
                    Directory.EnumerateFiles(Server.MapPath("~/Content/ArtImages")).Select(
                        fn => "~/Content/ArtImages/" + Path.GetFileName(fn)).ToList())
            {
                ViewData[imageA.Split('/')[3].Split('.')[0] + "Art"] = imageA;
            }
            return View(new ArtImageViewModel{Art = artImage,ReturnUrl = returnUrl});
        }

        
        [HttpPost]
        public ActionResult ChangePostPageSize(int size)
        {
            PageSize = size;
            return List();
        }

    }
}
