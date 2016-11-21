using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAndCAssignment.Models
{
    public class LineItem
    {
        public int Quantity { get; set; }
        /* OrderID should be the session ID */
        public string OrderID { get; set; }
        public string ProductID { get; set; }
    }
}