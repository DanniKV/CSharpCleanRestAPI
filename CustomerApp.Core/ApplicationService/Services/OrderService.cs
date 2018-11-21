using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {

        //Dependency
        readonly IOrderRepository _orderRepository;
        readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        //

        public Order New()
        {
            return new Order();
        }

        public Order CreateOrder(Order order)
        {
            if (order.Customer == null || order.Customer.Id <= 0)
                throw new InvalidDataException("To Create an Order you need a Customer!");
            if (_customerRepository.ReadyById(order.Customer.Id) == null)
                throw new InvalidDataException("Customer not found");
            if (order.OrderDate == null)
                throw new InvalidDataException("The Order Needs a Date");
            return _orderRepository.Create(order);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepository.Delete(id);
        }

        public Order FindOrderById(int id)
        {
            return _orderRepository.ReadyById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.ReadAll().ToList();
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            return _orderRepository.Update(orderUpdate);
        }

        public List<Order> GetFilteredOrders(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new InvalidDataException("Current Page or Item Page Can't be 0!");
            }
            if ((filter.CurrentPage -1 * filter.ItemsPerPage) >= _orderRepository.Count())
            {
                throw new InvalidDataException("Index too high, no available pages!");
            }
            return _orderRepository.ReadAll(filter).ToList();
        }
    }
}
