using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {

        public IEnumerable<Genre> Genres { get; set; }

        public Movie Movie { get; set; }


        // Use this logic to set title for this model view, if movie is in database title is set to edit movie otherwise is new movie
        public string Title
        {
            get
            {
                if (Movie != null && Movie.Id != 0)
                    return "Edit Movie";
               
                    return "New Movie";

               
            }
        }

    }
}