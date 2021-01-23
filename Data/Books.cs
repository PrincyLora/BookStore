using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace practise.BookStoreApp.Data
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int LanguageId { get; set; }

        [Required]
        public int Pages { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Language Language { get; set; }

        public string CoverImageURL { get; set; }

        public ICollection<BookGallery> bookGallery { get; set; }
    }

   
}
