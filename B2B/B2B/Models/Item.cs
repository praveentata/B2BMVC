using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B2B.Models;

namespace B2B.Models
{
    public class Item
    {
        private Products product = new Products();

        public Products Product
        {
            get { return product; }
            set { product = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Item() { }

        public Item(Products p, int q)
        {
            product = p;
            quantity = q;
        }
    }
}