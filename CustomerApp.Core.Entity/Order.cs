using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DeliverDate {get; set;}

        public DateTime OrderDate { get; set; }

        //Relations

        public Customer Customer { get; set; }

        /*
        public List<Order> Orders { get; set; }
        */



    }
}
