using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace POLBlog.Models
{
    public class BaseListsViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public List<SelectListItem> DrpPageSize { get; set; }
    }
}