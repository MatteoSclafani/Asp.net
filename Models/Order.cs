using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAndCAssignment.Models
{
    public class Order
    {
        public DateTime OrderDate { get; set; }
        /* OrderID should be populated with the SessionID */
        public string OrderID { get; set; }
        public string CustomerEmail { get; set; }
        public decimal ProductsPrice { set; get; }
    }
}