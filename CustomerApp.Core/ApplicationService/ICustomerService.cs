using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface ICustomerService
    {
        //New Customer
        Customer NewCustomer(string firstName,
                                string lastName,
                                string address);

        //Create/Post
        Customer CreateCustomer(Customer cust);
        //Read/Get
        Customer FindCustomerById(int id);
        Customer FindCustomerByIdIncludeOrders(int id);
        List<Customer> GetAllCustomers();
        List<Customer> GetAllByFirstName(string name);
        //Update/Put
        Customer UpdateCustomer(Customer customerUpdate);
        
        //Delete
        Customer DeleteCustomer(int id);
    }
}
