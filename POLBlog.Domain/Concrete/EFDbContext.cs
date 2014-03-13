using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using POLBlog.Domain.Entities;

namespace POLBlog.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Art> Arts { get; set; } 
    }
}
