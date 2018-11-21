using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DKVRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_orderService.GetFilteredOrders(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //return Ok(_orderService.GetAllOrders());

        }

        // GET api/Orders/5 Read by Id
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            if (id < 1) return BadRequest("Id Value Must Be Above 0");

            return Ok(_orderService.FindOrderById(id));
        }

        // POST api/Orders
        [HttpPost] //Create
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
            
        }

        // PUT api/Orders/5
        [HttpPut("{id}")] //Update
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.Id)
            {
                return BadRequest("Parameter Id and order Id must Match!");
            }
            return Ok(_orderService.UpdateOrder(order));
        }

        // DELETE api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            return Ok("Customer with ID: {id} is deleted!");
        }
    }
}