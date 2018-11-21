using System;
using System.Collections.Generic;
using System.Text;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data
{   //Connection Point to the Database, only contains parts of the DB
    //Only contains content that we are working with at a given time
    public class CustomerAppContext: DbContext
    {
        //SuperClass 
        //Options for database: Sql, in-Memory and so on...
        public CustomerAppContext(DbContextOptions<CustomerAppContext> Opt) : base(Opt)
        {
           
        }
        //Fluint API Model-Builder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.SetNull); //Removes the cascade delete effect when deleting customers


            /*
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(c => c.Customer.Id);
                */
          
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
