using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAndCAssignment.Models;

namespace WAndCAssignment.Controllers
{
    public class Item
    {

        private Products product = new Products();

        private int quantity;

        //Constructor

        public Item() { }

        public Item(Products product, int quantity)
        {
            this.product = product;

        }

        // encapsulating fields product and quantity
        public Products Product
        {
            get
            {
                return product;
            }

            set
            {
                product = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
    }
}