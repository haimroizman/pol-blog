using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POLBlog.Domain.Entities;

namespace POLBlog.Domain.Abstract
{
    public interface IArtsRepository
    {
        IQueryable<Art> Arts { get; }

        int SaveArt(Art art);

        void DeleteArt(Art art);
    }
}
