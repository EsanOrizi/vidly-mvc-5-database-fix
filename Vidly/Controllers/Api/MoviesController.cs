using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {


        // Get movies from database
        private ApplicationDbContext _context;
        // initialize it
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }



        //--------------------------------------------
        // get a list of movies
        // GET /api/movies
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }






        //--------------------------------------------
        // get a single movie
        // GET /api/movies/1
        public Movie GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return movie;
        }


        //--------------------------------------------
        // save a movie
        // POST /api/movies
        [HttpPost]
        public Movie CreateMovie(Movie movie)
        {

            // masking sure movie is valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // if valid save 
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return movie;

        }




        //--------------------------------------------
        // update a movie
        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, Movie movie)
        {

            // masking sure movie is valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // get the movie 
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            // make sure movie is not null
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // if not null  then save
            movieInDb.Id = movie.Id;
            movieInDb.Name = movie.Name;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.DateAdded = movie.DateAdded;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.NumberInStock = movie.NumberInStock;

            // save changes
            _context.SaveChanges();

        }



        //--------------------------------------------
        // delete a movie
        // DELETE /api/movies/1

        public void DeleteMovie(int id)
        {

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();


        }






    }
}
