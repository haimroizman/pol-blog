using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POLBlog.Domain.Entities;

namespace POLBlog.Domain.Abstract
{
    public interface IPostsRepository
    {
        IQueryable<Post> Posts { get; }

        int SavePost(Post post);

        void DeletePost(Post post);
    }
}
