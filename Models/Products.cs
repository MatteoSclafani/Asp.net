using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAndCAssignment.Models
{
    public class Products
    {
        public string ProductID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductBrand { get; set; }
        public string ProductModel { get; set; }
        public ProductType ProductType { set; get; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }

    }
}