﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POLBlog.Domain.Entities;

namespace POLBlog.Models
{
    public class ArtImageViewModel
    {
        public Art Art { get; set; }
        public string ReturnUrl { get; set; }
    }
}