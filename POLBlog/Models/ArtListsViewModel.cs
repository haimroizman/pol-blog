using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POLBlog.Domain.Entities;

namespace POLBlog.Models
{
    public class ArtListsViewModel:BaseListsViewModel
    {
        public IEnumerable<Art> Arts { get; set; }
    }
}