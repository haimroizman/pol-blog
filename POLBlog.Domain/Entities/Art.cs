using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace POLBlog.Domain.Entities
{
    public class Art
    {
        [HiddenInput(DisplayValue = false)]
        public int ArtID { get; set; }

        [MaxLength(24, ErrorMessage = "The length of the title must be no more than 24 characters")]
        [Required(ErrorMessage = "Give your art image a title")]
        public string ArtTitle { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreationDate { get; set; }
    }

}
