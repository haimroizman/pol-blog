using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POLBlog.Models
{
    public class PostViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PostID { get; set; }

        public string Name { get; set; }

        public string ArticleBody { get; set; }

        public string Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreationDate { get; set; }

       // public string ImagePath { get; set; }
    }
}