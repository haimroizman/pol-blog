using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace POLBlog.Domain.Entities
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        public int PostID { get; set; }

        [Required(ErrorMessage ="Please Mr Yair enter a post name" )]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mr Yair, what about the article ?")]
        [DataType(DataType.MultilineText)]
        [MinLength(300,ErrorMessage = "Article body must be at least with 300 characters")]
        public string ArticleBody { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreationDate { get; set; }
    }
}
