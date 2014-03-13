using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using POLBlog.Domain.Abstract;
using POLBlog.Domain.Entities;

namespace POLBlog.Domain.Concrete
{
    public class EFPostRepository:IPostsRepository
    {
        private EFDbContext context=new EFDbContext();

        public IQueryable<Post> Posts
        {
            get { return context.Posts; }
        }

        public int SavePost(Post post)
        {
            int result = 0;
            if(post.PostID==0)
            {
                context.Posts.Add(post);
                HttpRuntime.Cache.Remove("Posts");
            }
            else
            {
                context.Entry(post).State=EntityState.Modified;
                HttpRuntime.Cache.Remove("Posts");
                HttpRuntime.Cache.Remove("Post" + post.PostID);
            }

            context.SaveChanges();
            result = post.PostID;
            return result;
        }

        
        public void DeletePost(Post post)
        {
            context.Posts.Remove(post);
            context.SaveChanges();
            HttpRuntime.Cache.Remove("Posts");
        }

    }
}
