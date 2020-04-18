using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using Vidly.Dtos;
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
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {


            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);



          }






        //--------------------------------------------
        // get a single movie
        // GET /api/movies/1
        public MovieDto GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Movie, MovieDto>(movie);
        }


        //--------------------------------------------
        // save a movie
        // POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {

            // masking sure movie is valid
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            // if valid save 
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }




        //--------------------------------------------
        // update a movie
        // PUT /api/movies/1
        [HttpPut]
        
        [Authorize(Roles = RoleName.CanManageMovies)]

        public void UpdateMovie(int id, MovieDto movieDto)
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

            Mapper.Map(movieDto, movieInDb);

            // save changes
            _context.SaveChanges();

        }



        //--------------------------------------------
        // delete a movie
        // DELETE /api/movies/1

        [Authorize(Roles = RoleName.CanManageMovies)]
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
