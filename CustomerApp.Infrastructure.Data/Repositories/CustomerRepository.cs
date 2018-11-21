using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly CustomerAppContext _ctx;

        public CustomerRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }

        public Customer Create(Customer customer)
        {
            var cust = _ctx.Customers.Add(customer).Entity;
            _ctx.SaveChanges();
            return cust;

            //Old system
            /*customer.Id = id++;
            FakeDB.Customers.Add(customer);//_customers.Add(customer);
            return customer;*/
        }

        public Customer Delete(int id)
        {
            /*
            //To remove all orders attached to the specific customer
            var ordersToRemove = _ctx.Orders.Where(o => o.Customer.Id == id);
            _ctx.RemoveRange(ordersToRemove);
            */

            //Doesn't work for customers with orders!
            //Fluint API makes it work without constraints.. Relational mapping
            var custRemoved = _ctx.Remove(new Customer { Id = id }).Entity;
            _ctx.SaveChanges();
            return custRemoved;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _ctx.Customers;
            
            
            //Old Static in-memory system FakeDB
            //return FakeDB.Customers;
        }

        public Customer ReadyById(int id)
        {
            //var changeTracker = _ctx.ChangeTracker.Entries<Customer>(); //very smart for debugging
            return _ctx.Customers.FirstOrDefault(c => c.Id == id);



            //Old Static system

            /*return FakeDB.Customers.
                Select(c => new Customer()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address
                }).
                FirstOrDefault(c => c.Id == id);*/
        }

        public Customer ReadyByIdIncludeOrders(int id)
        {
            return _ctx.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.Id == id);


            /* OLD method
            customer.Orders = _orderRepository.ReadAll()
                .Where(order => order.Customer.Id == customer.Id).ToList();

            return customer;
            */
        }
        //For some reason, update not functioning propperly atm.
        public Customer Update(Customer customerUpdate)
        {
            _ctx.Attach(customerUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return customerUpdate;
        }
    }
}
