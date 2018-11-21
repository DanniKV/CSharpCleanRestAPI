using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.ApplicationService
{
    public interface IOrderService
    {
        //New Order
        Order New();

        //Create/Post
        Order CreateOrder(Order order);

        //Read/Get
        Order FindOrderById(int id);
        List<Order> GetAllOrders();
        List<Order> GetFilteredOrders(Filter filter);

        //Update/Put
        Order UpdateOrder(Order orderUpdate);

        //Delete
        Order DeleteOrder(int id);

        
    }
}
