using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Models
{
    public class ProductsSearch
    {
        private List<Products> _products;

        public List<Products> Products
        {
            get { return _products; }
            set { _products = value; }
        }
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value; }
        }
    }
}