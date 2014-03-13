using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POLBlog.Domain.Abstract;
using POLBlog.Domain.Entities;

namespace POLBlog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IPostsRepository postsRepository;
        private IArtsRepository artsRepository;

        public AdminController(IPostsRepository postRepo,IArtsRepository artsRepo)
        {
            postsRepository = postRepo;
            artsRepository = artsRepo;
        }

        public ViewResult ShowPosts()
        {
            ViewBag.SelectedIssue = "PostsAdmin";
            return View(postsRepository.Posts);
        }

        public ViewResult Edit(int PostID)
        {
            Post post = postsRepository.Posts.FirstOrDefault(p => p.PostID == PostID);

            foreach (var imageP in Directory.EnumerateFiles(Server.MapPath("~/Content/PostImages"))
                .Select(fn => "~/Content/PostImages/" + Path.GetFileName(fn)).ToList())
            {
                ViewData[imageP.Split('/')[3].Split('.')[0]] = imageP;
            }


            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if(ModelState.IsValid)
            {
                post.CreationDate = DateTime.Now;
                int idFileName=postsRepository.SavePost(post);

                foreach (string file in Request.Files)
                {
                    var uploadedFile = this.Request.Files[file];
                    if (uploadedFile != null && uploadedFile.ContentLength > 0)
                    {
                        if (uploadedFile.ContentLength > 1024*1024*4)
                        {
                            ModelState.AddModelError("uploadError", "File size can't exceed 4 MB");
                            return View(post);
                        }
                        

                        string fileName = uploadedFile.FileName;
                        string[] splitFileName = fileName.Split('.');
                        fileName =
                            Path.GetFileName(idFileName.ToString() + '.' + splitFileName[splitFileName.Length - 1]);

                        var path = Path.Combine(Server.MapPath("~/Content/PostImages/"), fileName);

                        try
                        {
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);

                            uploadedFile.SaveAs(path);
                        }
                        catch (Exception)
                        {

                            ModelState.AddModelError("uploadError", "Can't save file to disk");
                        }
                        
                    }

                }
                
                TempData["message"] = string.Format("{0} has been saved", post.Name);
                TempData["selectedIsuue"] = "PostsAdmin";
                return RedirectToAction("ShowPosts");
            }
            else
            {
                // there is something wrong with the data values
                return View(post);
            }
        }

        [HttpPost]
        public ActionResult Delete(int PostID)
        {
            Post postToDelete = postsRepository.Posts.FirstOrDefault(p => p.PostID == PostID);
            if(postToDelete!=null)
            {
                postsRepository.DeletePost(postToDelete);
                TempData["message"] = string.Format("{0} was deleted", postToDelete.Name);
                ViewBag.SelectedIssue = "PostsAdmin";
            }

            return RedirectToAction("ShowPosts");
        }

        public ViewResult Create()
        {
            return View("Edit", new Post());
        }

        public ViewResult ShowArts()
        {
            return View(artsRepository.Arts);
        }

        public ViewResult EditArt(int ArtID)
        {
            Art art = artsRepository.Arts.FirstOrDefault(a => a.ArtID == ArtID);

            foreach (var imageA in Directory.EnumerateFiles(Server.MapPath("~/Content/ArtImages"))
                .Select(fn => "~/Content/ArtImages/" + Path.GetFileName(fn)).ToList())
            {
                ViewData[imageA.Split('/')[3].Split('.')[0]] = imageA;
            }


            return View(art);
        }


        [HttpPost]
        public ActionResult EditArt(Art art)
        {
            if (ModelState.IsValid)
            {
                art.CreationDate = DateTime.Now;
                

                foreach (string file in Request.Files)
                {
                    var uploadedFile = this.Request.Files[file];
                    if (uploadedFile != null && uploadedFile.ContentLength > 0)
                    {
                        if (uploadedFile.ContentLength > 1024 * 1024 * 4)
                        {
                            ModelState.AddModelError("uploadError", "File size can't exceed 4 MB");
                            return View(art);
                        }


                        string fileName = uploadedFile.FileName;
                        string[] splitFileName = fileName.Split('.');
                        int idFileName = artsRepository.SaveArt(art);
                        fileName =
                            Path.GetFileName(idFileName.ToString() + '.' + splitFileName[splitFileName.Length - 1]);

                        var path = Path.Combine(Server.MapPath("~/Content/ArtImages/"), fileName);

                        try
                        {
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);

                            uploadedFile.SaveAs(path);
                        }
                        catch (Exception)
                        {

                            ModelState.AddModelError("uploadError", "Can't save file to disk");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("uploadError", "You must upload an art image in this art section");
                        return View(art);
                    }
                }

                TempData["message"] = string.Format("{0} has been saved", art.ArtTitle);
                return RedirectToAction("ShowArts");
            }
            else
            {
                // there is something wrong with the data values
                return View(art);
            }
        }

        [HttpPost]
        public ActionResult DeleteArt(int ArtID)
        {
            Art artToDelete = artsRepository.Arts.FirstOrDefault(a => a.ArtID == ArtID);
            if (artToDelete != null)
            {
                artsRepository.DeleteArt(artToDelete);
                TempData["message"] = string.Format("{0} was deleted", artToDelete.ArtTitle);
            }

            return RedirectToAction("ShowArts");
        }

        public ViewResult CreateArt()
        {
            return View("EditArt", new Art());
        }
    }
}
