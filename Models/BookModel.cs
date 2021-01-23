using Microsoft.AspNetCore.Http;
using practise.BookStoreApp.Enum;
using practise.BookStoreApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace practise.BookStoreApp.Models
{
    public class BookModel
    {
        [DataType(DataType.DateTime)]
        public string MyField { get; set; }
        public int Id { get; set; }

        [Required]
        // [StringLength(100, MinimumLength =5)]
       // [MyCustomValidationAttributes(ErrorMessage ="custom message from model")]
       [MyCustomValidationAttributes("azure")]
        public string Title { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        [Required]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        //[Required(ErrorMessage = "Please select languages!")]
        //public List<string> MultiLanguange { get; set; }

        [Display(Name ="Select a Language")]
        [Required(ErrorMessage = "Please select languages!")]
        public LanguageEnum LanguageEnum { get; set; }

        [Required(ErrorMessage = "Enter number of pages!")]
        public int? Pages { get; set; }

        [Display(Name ="Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageURL { get; set; }

        [Display(Name = "Choose the cover photos of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }
    }
}
