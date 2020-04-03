﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }
        [Required]

        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        
        
        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }
    }
}