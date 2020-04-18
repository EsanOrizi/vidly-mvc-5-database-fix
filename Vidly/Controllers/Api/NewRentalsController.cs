using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        // get rentals from database
        // create _context
        private ApplicationDbContext _context;

        // initialize it
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // HttpPost, we creating new objects and to comply with RESTful 
        // input into our application is NewRentalDto 

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            // get the customer from _context
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);
            // get the movies
            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();





            // iterate over the movies
            foreach (var movie in movies)
            {

                // make sure movie is available 
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");


                // deduct number NumberAvailable for each movies object creates
                movie.NumberAvailable--;
                // for each movie create a new rental object
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                // add the rental to _context
                _context.Rentals.Add(rental);

            }


            _context.SaveChanges();

            return Ok();

        }




    }
}
