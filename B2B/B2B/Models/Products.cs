using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B2B.Models
{
    public class Products
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private string productDescription;

        public string ProductDescription
        {
            get { return productDescription; }
            set { productDescription = value; }
        }
        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        private int price;

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private int supplierId;

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        private string supplierName;

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }
        //private bool selected;

        //public bool Selected
        //{
        //    get { return selected; }
        //    set { selected = value; }
        //}


    }
}