using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using POLBlog.Domain.Abstract;
using POLBlog.Domain.Entities;

namespace POLBlog.Domain.Concrete
{
    public class EFArtRepository:IArtsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Art> Arts
        {
            get { return context.Arts; }
        }

        
        public int SaveArt(Art art)
        {
            int result = 0;
            if (art.ArtID == 0)
            {
                context.Arts.Add(art);
                HttpRuntime.Cache.Remove("Arts");
            }
            else
            {
                context.Entry(art).State = EntityState.Modified;
                HttpRuntime.Cache.Remove("Arts");
                HttpRuntime.Cache.Remove("Arts" + art.ArtID);
            }

            context.SaveChanges();
            result = art.ArtID;
            return result;
        }

        public void DeleteArt(Art art)
        {
            context.Arts.Remove(art);
            context.SaveChanges();
            HttpRuntime.Cache.Remove("Arts");
        }
    }
}
