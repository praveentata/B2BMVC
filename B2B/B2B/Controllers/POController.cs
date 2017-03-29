using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B2B.Models;


namespace B2B.Controllers
{
    public class POController : Controller
    {
      

        public ActionResult po()
        {
            MyEntities ME = new MyEntities();

            int user_id = Convert.ToInt32(Session["userId"].ToString()); // logged in user.

            var poItems = ME.poes.Where(x => x.user_id == user_id).ToList(); // finding POs for the logged in user.

            return View(poItems); // returning POs for the logged in user.
        }

        
        public ActionResult Invoice(string search) // searching for POs on suppliers PO page.
        {
            MyEntities ME = new MyEntities();
            List<supplier_inv> list = new List<supplier_inv>();
            int id = 0;

            if (search == null || search == "")
            {
                list = ME.supplier_inv.ToList();
            }
            else if(int.TryParse(search, out id))
            {
                list = ME.supplier_inv.Where(x => x.PO_ID == id).ToList(); // searching 
            }
             
            else if(search != null)
            {
                list = ME.supplier_inv.Where(x => x.ProductName == search || x.status == search).ToList();
            }
           

            
            int user_id = Convert.ToInt32(Session["userId"].ToString());
            
            
            
            List<supplier_inv> newList = new List<supplier_inv>();
            if (ModelState.IsValid)
            {
                    var result = list.GroupBy(x => x.PO_ID).ToList(); // Products with same supplier need to have same PO, so group by PO ID.

                    foreach (var re in result)
                    {
                        supplier_inv si = new supplier_inv();
                        si.PO_ID = re.Key;
                        si.Price = re.Sum(x => x.Price); // sum
                        si.quantity = re.Count(x => x.PO_ID == si.PO_ID);
                        si.user_id = Convert.ToInt32(Session["userId"].ToString());

                        foreach(var r in re) // this loop is for status.
                        {
                            si.status = r.status; // status was set for PO and not for products, that's why this is inside a group, so using loop.
                        }

                        si.shopping_date = DateTime.Now;
                        newList.Add(si);
                    }
                
            }

            return View(newList); // returning the invoice list to the view.
        }

        public ActionResult Details(int id) // viewing details of an Invoice. Will take the PO ID.
        {
            MyEntities ME = new MyEntities();
            int user_id = Convert.ToInt32(Session["userId"].ToString());
            var suppliersItems = ME.poes.Where(x => x.PO_ID == id).ToList(); // searching the PO with ID
            List<supplier_inv> list = new List<supplier_inv>();
            if (ModelState.IsValid)
            {
                using (MyEntities db = new MyEntities())
                {
                    foreach (var s in suppliersItems)
                    {
                        supplier_inv si = new supplier_inv();
                        si.PO_ID = s.PO_ID;
                        si.user_id = s.user_id;
                        si.status = s.status;
                        si.product_id = s.product_id;
                        si.SupplierId = s.SupplierId;
                        si.ProductName = s.ProductName;
                        si.ProductDescription = s.ProductDescription;
                        si.Image = s.Image;
                        si.Price = s.Price;
                        si.shopping_date = s.shopping_date;
                        si.quantity = s.quantity;
                        list.Add(si);
                        
                    }

                }
            }

            return View(list); // returning to list.
        }

        public ActionResult sendPO() // sending PO to supplier.
        {

            deleteFromShoppingCart(); // once the PO is sent to the supplier, all products from user's cart will get deleted.

            MyEntities ME = new MyEntities();
            int user_id = Convert.ToInt32(Session["userId"].ToString());
            var suppliersItems = ME.poes.Where(x => x.user_id == user_id).ToList(); // searching with user id for PO.
            List<supplier_inv> list = new List<supplier_inv>();
            List<supplier_inv> newList = new List<supplier_inv>();
            if(ModelState.IsValid)
            {
                using(MyEntities db = new MyEntities())
                {
                    foreach(var s in suppliersItems)
                    {
                        supplier_inv si = new supplier_inv(); // adding to supplier list.
                        si.PO_ID = s.PO_ID;
                        si.user_id = s.user_id;
                        si.status = s.status;
                        si.product_id = s.product_id;
                        si.SupplierId = s.SupplierId;
                        si.ProductName = s.ProductName;
                        si.ProductDescription = s.ProductDescription;
                        si.Image = s.Image;
                        si.Price = s.Price;
                        si.shopping_date = s.shopping_date;
                        si.quantity = s.quantity;
                        list.Add(si);
                        db.supplier_inv.Add(si);
                        db.SaveChanges(); // saving changes to DB.

                    }
                    var result = list.GroupBy(x => x.PO_ID).ToList(); // grouping by PO ID. [As explained above].

                    foreach(var re in result)
                    {
                        supplier_inv si = new supplier_inv(); // adding supplier invoice list.
                        si.PO_ID = re.Key;
                        si.Price = re.Sum(x => x.Price);
                        si.quantity = re.Count(x => x.PO_ID == si.PO_ID);
                        si.user_id = Convert.ToInt32(Session["userId"].ToString());
                        si.status = "Pending";
                        si.shopping_date = DateTime.Now;
                        newList.Add(si);
                    }

                    ViewBag.Message = "Congratulations! Your POs with following numbers have been sent to respective suppliers.";
                    
                }
            }

            return View(newList); // returning list to the view.
        }

        public void deleteFromShoppingCart() // deleting all the products for the logged in user from the cart.
        {
            int user_id = Convert.ToInt32(Session["userId"].ToString());
            using (MyEntities db = new MyEntities())
            {
                shopping_cart sc = new shopping_cart();
                var sCart = db.shopping_cart.Where(x => x.user_id == user_id).ToList();
                for (int i = 0; i < sCart.Count; i ++ )
                {
                    db.shopping_cart.Remove(sCart[i]);
                }
                    
            }
        }

    }
}
