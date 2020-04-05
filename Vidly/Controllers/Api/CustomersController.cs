using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<Customer> GetCustomers()
        {   
            // return customers list
            return _context.Customers.ToList();
        }



        //--------------------------------------------
        // get a single customer
        // GET /api/customers/1
        public Customer GetCustomer(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // if customer is not found
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            // otherwise 
            return customer;
        }



        //--------------------------------------------
        // save a customer
        // POST /api/customers
        // 
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            // validate the customer
            if (!ModelState.IsValid)
                throw  new HttpResponseException(HttpStatusCode.BadRequest);

            // otherwise add the customer
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }



        //--------------------------------------------
        // update a customer
        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            // make sure input is valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // get the customer from database
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            // see if it exists
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

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
