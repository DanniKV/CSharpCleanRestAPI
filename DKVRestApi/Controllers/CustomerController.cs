using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerApp.Core.Entity;
using CustomerApp.Core.ApplicationService;

namespace DKVRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _CustomerService;

        public CustomersController(ICustomerService customerService)
        {
            _CustomerService = customerService;
        }

        // GET api/Customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _CustomerService.GetAllCustomers();
            
        }

        // GET api/Customers/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id < 1) return BadRequest("Id Value Must Be Above 0");

            return _CustomerService.FindCustomerByIdIncludeOrders(id);
            //return _CustomerService.FindCustomerById(id);
        }

        // POST api/Customers
        [HttpPost] //Create
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                return BadRequest("Need a First Name");
            }
            if (string.IsNullOrEmpty(customer.LastName))
            {
                return BadRequest("Need a Last Name");
            }
            return _CustomerService.CreateCustomer(customer);
        }

        // PUT api/Customers/5
        [HttpPut("{id}")] //Update
        public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
        {
            if (id < 1 || id != customer.Id)
            {
                return BadRequest("Customer Id and Parameter Id must match!");
            }
            return Ok(_CustomerService.UpdateCustomer(customer));
        }

        // DELETE api/Customers/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = _CustomerService.DeleteCustomer(id);
            if (customer == null)
            {
                return StatusCode(404, "didn't find Customer with ID " + id);
            }
            return Ok($"Customer with ID: {id} is deleted!");
        }

    }
}
