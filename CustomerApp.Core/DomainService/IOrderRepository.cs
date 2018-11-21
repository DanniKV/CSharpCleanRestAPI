using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.DomainService
{
    public interface IOrderRepository
    {
        //OrderRepository Interface
        //Create Data
        //No Id when enter, but Id when exits
        Order Create(Order order);

        //Read Data
        Order ReadyById(int id);
        IEnumerable<Order> ReadAll(Filter filter = null);

        //Update Data
        Order Update(Order orderUpdate);

        //Delete Data
        Order Delete(int id);

        //For Paging
        int Count();
    }
}
