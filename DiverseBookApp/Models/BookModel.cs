using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DiverseBookApp.Enums;
using DiverseBookApp.Helpers;
using Microsoft.AspNetCore.Http;

namespace DiverseBookApp.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter Book Title!")]
        //[MyCustomValidation(ErrorMessage ="This is error message from custom validation!")]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        [Required]
        [Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }

        [Required]
        [Display(Name ="Choose Cover Photo of your book")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Required]
        [Display(Name = "Choose Gallery Image of your book")]
        public IFormFileCollection GalleryImages { get; set; }

        public List<GalleryImagesModel> Gallery { get; set; }
    }
}
