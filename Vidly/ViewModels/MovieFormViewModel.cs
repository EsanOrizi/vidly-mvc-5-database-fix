using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {

        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }


        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }


        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }


        // Use this logic to set title for this model view, if movie is in database title is set to edit movie otherwise is new movie
        public string Title
        {
            get
            {
                if (Id != 0)
                    return "Edit Movie";

                return "New Movie";


            }
        }



        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.GenreId;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }

    }
}