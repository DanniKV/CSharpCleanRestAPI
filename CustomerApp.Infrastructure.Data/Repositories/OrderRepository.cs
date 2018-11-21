using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using CustomerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly CustomerAppContext _ctx;

        public OrderRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }

        

        public Order Create(Order order)
        {

            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
            /*
            // var changeTracker = _ctx.ChangeTracker.Entries<Customer>(); //very smart for debugging
            if (order.Customer != null &&
                _ctx.ChangeTracker.Entries<Customer>()
                .FirstOrDefault(ce => ce.Entity.Id == order.Customer.Id) == null)
            {
                //Attaches order to the given customer with the input ID (PostMan)
                //instead of creating a new customer with that ID
                _ctx.Attach(order.Customer);  
            }
            var OrderSaved = _ctx.Orders.Add(order).Entity;
            _ctx.SaveChanges();
            return OrderSaved;
            */
        }

        public Order Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadAll(Filter filter)
        {
            if (filter == null)
            {
                return _ctx.Orders;
            }
            return _ctx.Orders
                .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage);
        }

        public Order ReadyById(int id)
        {
            return _ctx.Orders.Include(o => o.Customer)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order Update(Order orderUpdate)
        {
            _ctx.Attach(orderUpdate).State = EntityState.Modified;
            _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            _ctx.SaveChanges();
            return orderUpdate;


            /*
            // var changeTracker = _ctx.ChangeTracker.Entries<Customer>(); //very smart for debugging
            if (orderUpdate.Customer != null &&
                _ctx.ChangeTracker.Entries<Customer>() //for Disconnected Entity
                .FirstOrDefault(ce => ce.Entity.Id == orderUpdate.Customer.Id) == null)
            {
                //Attaches order to the given customer with the input ID (PostMan)
                //instead of creating a new customer with that ID
                _ctx.Attach(orderUpdate.Customer);
            }
            else
            {
                //To remove customer from the order in an update request
                _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            }
            var Updated = _ctx.Orders.Update(orderUpdate).Entity;
            _ctx.SaveChanges();
            return Updated;
            */
        }

        //For Paging
        public int Count()
        {
            return _ctx.Orders.Count();
        }
    }
}
