using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POLBlog.Domain.Entities;

namespace POLBlog.Models
{
    public class PostsListsViewModel : BaseListsViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        
        //public IEnumerable<string> Images { get; set; }
    }
}