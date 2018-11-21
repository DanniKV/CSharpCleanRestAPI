using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(CustomerAppContext ctx)
        {
            //This ensures that the DB is deleted. before seeding
            //MAKE SURE TO BE UNDER THE SECTION "env.IsDevelopment()" 
            //else it can delete complete production DB !!!DØD BABY!!!

            ctx.Database.EnsureDeleted();

            //Ensures the DB is created with the startup customers and orders
            //Seeds the Data into the DB - stacks with each startup
            ctx.Database.EnsureCreated();




            //Customers
            var cust1 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 1,
                FirstName = "Danni",
                LastName = "Vase",
                Address = "ShizzleStreet 123"

            }).Entity;
            var cust2 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                FirstName = "Bingo",
                LastName = "Bango Bongo",
                Address = "BishBashBosh 123"
            }).Entity;
            var cust3 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                FirstName = "Lars",
                LastName = "Ulrich",
                Address = "MetalSt. 112"
            }).Entity;
            var cust4 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                FirstName = "Flemming",
                LastName = "Knudsen",
                Address = "FlamingoSt. 321"
            }).Entity;
            var cust5 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                FirstName = "Winston",
                LastName = "Churchill",
                Address = "BritainSt. 232"
            }).Entity;
            var cust6 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                FirstName = "Bruce",
                LastName = "Dickinson",
                Address = "MaidenSt. 666"
            }).Entity;
            var cust7 = ctx.Customers.Add(new Customer()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                FirstName = "Adrian",
                LastName = "Smith",
                Address = "MaidenSt 1337"
            }).Entity;

            //Orders
            ctx.Orders.Add(new Order()
            {
                //ID Constraints with SQLite DB
                //Id = 1,
                OrderDate = DateTime.Now,
                DeliverDate = DateTime.Now,
                Customer = cust1
            });
            ctx.Orders.Add(new Order()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                OrderDate = DateTime.Now,
                DeliverDate = DateTime.Now,
                Customer = cust1
            });
            ctx.Orders.Add(new Order()
            {
                //ID Constraints with SQLite DB
                //Id = 2,
                OrderDate = DateTime.Now,
                DeliverDate = DateTime.Now,
                Customer = cust2
            });

            ctx.SaveChanges();
        }
    }
}
