using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B2B.Models;
using System.Data.SqlClient;


namespace B2B.Controllers
{
    public class ProductsController : Controller
    {
        bool skip = false;
        //
        // GET: /Products/
        static List<Products> listOfProducts = new List<Products>(); // Creating a list of Products to add query results to the list.
        public ActionResult products(string searchTerm, string dropDownValue)
        {
            
            string sortValue = "";
            int sID = 0;
            dropDown();
            //sorting();
            
          
            
            if (Request.HttpMethod == "POST") // when searching something this will execute.
            {
                dropDownValue =  Request.Form["Suppliers"].ToString(); // get the value from dropdown.
                //sortValue = Request.Form["Sortby"].ToString(); // Request.Form will take the page name and 'Supplier' is the name of the dropdown -> will get the value from that.
                
                if(int.TryParse(dropDownValue, out sID)) // if the value of dropdown is an integer, then it will store thatn in the variable for searching
                {
                    sID = int.Parse(dropDownValue);
                }
            }

            if (Request.HttpMethod == "GET" && listOfProducts.Count == 0) // if request is get and count of list is 0
            {                                                             // i.e. on first time page load, it will load all the products.
                ProductsContext pc = new ProductsContext();
                listOfProducts = pc.Products.ToList();
                
                // Connection String to connect to DB
                string connectionString = @"server=(localdb)\MSSQLLocalDB; database=B2B; integrated security=SSPI";
                string sql = "SELECT * from products"; // Query

            }
            else if(Request.HttpMethod == "GET")
            {
                skip = true;
            }
            else
            {
                listOfProducts.Clear(); // clearing the list on search action and adding only relevant products
                listOfProducts = productsSearch(searchTerm, sID); // this function will be called.
            }

            if(Request.HttpMethod == "Post" && sortValue != null) // if POST and sortValue has something then relevant function will be called.
            {
                if (sortValue.Equals("SortByPrice"))
                {
                    listOfProducts = listOfProducts.OrderBy(x => x.Price).ToList();
                }
                else if(sortValue.Equals("SortByName"))
                {
                    listOfProducts = listOfProducts.OrderBy(x => x.ProductName).ToList();
                }
                
            }
                   return View(listOfProducts); // return the view by passing the list.
                
            
            }

        public void sorting()  // function to poplate the sort by list.
        {

            List<SelectListItem> sort = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Sort by Name", Value = "SortByName"},
                new SelectListItem {Text = "Sort by Price", Value = "SortByPrice"}
            };

            ViewBag.Sort = sort; // adding the list to ViewBag.
        }

        public void dropDown() // getting values for suppliers from DB and adding them to the list.
        {
            B2BEntities db = new B2BEntities();
            ViewBag.Suppliers = new SelectList(db.Suppliers, "SupplierId", "SupplierName");
        }
        public ActionResult products1()
        {
            // Connection String to connect to DB
            string connectionString = @"server=(localdb)\MSSQLLocalDB; database=B2B; integrated security=SSPI";
            string sql = "SELECT * from products"; // Query


            // Connecting to DB.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn); // Creating command.
                conn.Open(); // opening connection

                SqlDataReader rdr = cmd.ExecuteReader(); // Executing SQL Command.
                var listOfProducts = new List<Products>(); // Creating a list of Products to add query results to the list.
                var p = new ProductsSearch();

                p.Products = new List<Products>();

                while (rdr.Read()) // while results are present
                {
                    
                    var prod = new Products(); // create a new object of the class and add.
                    prod.Id = (int)rdr["ID"];
                    prod.ProductName = (string)rdr["ProductName"];
                    prod.ProductDescription = (string)rdr["ProductDescription"];
                    prod.Image = (string)rdr["Image"];
                    prod.Price = (int)rdr["Price"];

                    listOfProducts.Add(prod); // add object reference variable to the list.

                    p.Products.Add(prod);
                    
                }
                
                return View(p); // return the view by passing the list.
            }
        } // ignore this function for now.


        

        [HttpPost] // POST because button will be clicked and data will be posted.
        public List<Products> productsSearch(string searchTerm, int supplierId) // searching 
        {
            string sql = "";
            // Connection String to connect to DB
            string connectionString = @"server=(localdb)\MSSQLLocalDB; database=B2B; integrated security=SSPI";

            if(supplierId == 0)
            {
                sql = "SELECT * from products where ProductName like '%" + searchTerm + "%' or ProductDescription like '%" + searchTerm + "%'"; // Query
            }
            else if(searchTerm == "")
            {
                sql = "SELECT * from products where SupplierId =  " + supplierId; // Query
            }
            else
            {
                sql = "SELECT * from products where ProductName like '%" + searchTerm + "%' or ProductDescription like '%" + searchTerm + "%' and SupplierId = " + supplierId; // Query
            }
            var listOfProduct = new List<Products>(); // Creating a list of Products to add query results to the list.

            // Connecting to DB.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn); // Creating command.
                conn.Open(); // opening connection

                SqlDataReader rdr = cmd.ExecuteReader(); // Executing SQL Command.


                while (rdr.Read()) // while results are present
                {
                    var prod = new Products(); // create a new object of the class and add.
                    prod.Id = (int)rdr["ID"];
                    prod.ProductName = (string)rdr["ProductName"];
                    prod.ProductDescription = (string)rdr["ProductDescription"];
                    prod.Image = (string)rdr["Image"];
                    prod.Price = (int)rdr["Price"];

                    listOfProduct.Add(prod); // add object reference variable to the list.
                }

            }

            return listOfProduct; // return the view by passing the list.

        }

    }

    
}
