using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POLBlog.Domain.Entities;

namespace POLBlog.Models
{
    public class PostFullArticleViewModel
    {
        public Post Post { get; set; }
        public string ReturnUrl { get; set; }
    }
}