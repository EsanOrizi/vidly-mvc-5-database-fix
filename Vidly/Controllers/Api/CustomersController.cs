using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
// add this import 
using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{

    // this class drives from ApiController instead of controller
    public class CustomersController : ApiController
    {

        
        
        //-------------------------------------------
        // get the database
        private ApplicationDbContext _context;
        // initialize it
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        


        //--------------------------------------------
        // get a list of customers
        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {   
            // return customers list
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
                var customerDto = customersQuery
                .ToList().Select(Mapper.Map<Customer, CustomerDto>);


            return Ok(customerDto);

        }



        //--------------------------------------------
        // get a single customer
        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // if customer is not found
            if (customer == null)
                return NotFound();
            // otherwise 
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }



        //--------------------------------------------
        // save a customer
        // POST /api/customers
        // 
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // validate the customer
            if (!ModelState.IsValid)
                return  BadRequest();

            // otherwise add the customer
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id),  customerDto);
        }



        //--------------------------------------------
        // update a customer
        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            // make sure input is valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // get the customer from database
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            // see if it exists
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
            

            _context.SaveChanges();
        }



        //--------------------------------------------
        // delete a customer
        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            
            // get the customer from database
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            // see if it exists 
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }



    }
}
