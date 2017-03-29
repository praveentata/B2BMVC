using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B2B.Models;

namespace B2B.Controllers
{
    public class ShoppingCartController : Controller
    {
        ProductsContext p = new ProductsContext();
        B2BEntities b = new B2BEntities();
        public ActionResult cart(int id)
        {
            bool check = false;
            List<Item> cart = new List<Item>();
            
            shopping_cart sc = new shopping_cart();

            if (Session["cart"] == null) // cart is empty.
            {

                if (product(id) != null) // if  found. 'product(int id) is a function defined in this controller further down.
                {
                    using (MyEntities me = new MyEntities())
                    {
                        shopping_cart s1 = new shopping_cart();

                        
                        int user_id = Convert.ToInt32(Session["userId"].ToString());
                        shopping_cart s = me.shopping_cart.SingleOrDefault(m => m.product_id == id && m.user_id == user_id);
                        s.quantity = s.quantity++; // finding and incrementing quantity.
                        s.Price = s.Price * s.quantity; // updating price.
                         
                        me.SaveChanges(); // save changes.
                        cart.Add(new Item(p.Products.Find(id), 1)); // Find will find the id and add 1 quantity.
                        Session["cart"] = cart;
                        check = true;

                        List<shopping_cart> result = me.shopping_cart.Where(x => x.user_id == 0).ToList(); //can't figure this out at the moment :P
                                                      

                        foreach (shopping_cart sCart in result)
                        {
                            me.shopping_cart.Remove(sCart); // removing from the cart.
                        }
                    }
                }
                else
                {
                    // if the product is not in the cart then add 1 quantity to the cart.
                    cart.Add(new Item(p.Products.Find(id), 1));
                    Session["cart"] = cart;

                    sc.user_id = Convert.ToInt32(Session["userId"].ToString());
                    sc.product_id = cart.FirstOrDefault().Product.Id;
                    sc.SupplierId = cart.FirstOrDefault().Product.SupplierId;
                    sc.ProductName = cart.FirstOrDefault().Product.ProductName;
                    sc.ProductDescription = cart.FirstOrDefault().Product.ProductDescription;
                    sc.Image = cart.FirstOrDefault().Product.Image;
                    sc.Price = cart.FirstOrDefault().Product.Price;
                    sc.shopping_date = DateTime.Now;
                    sc.quantity = 1;
                }
            }
            else
            {
                MyEntities myEntities = new MyEntities();
                cart = (List<Item>)Session["cart"];
                int index = isPresent(cart, id); // if cart is not empty, and product the user is trying to add is already present, get its index.
                
                if (index == -1) // -1 means the product is not present. Then add to cart.
                {
                    cart.Add(new Item(p.Products.Find(id), 1));
                    Item I = cart.Single(c => c.Product.Id == id);

                    sc.user_id = Convert.ToInt32(Session["userId"].ToString());
                    sc.product_id = I.Product.Id;
                    sc.SupplierId = I.Product.SupplierId;
                    sc.ProductName = I.Product.ProductName;
                    sc.ProductDescription = I.Product.ProductDescription;
                    sc.Image = I.Product.Image;
                    sc.Price = I.Product.Price;
                    sc.shopping_date = DateTime.Now;
                    sc.quantity = 1;
                }
                else // if present, increase quantity at that index.
                {
                    cart[index].Quantity++;

                    int user_id = Convert.ToInt32(Session["userId"].ToString());

                    using (MyEntities me = new MyEntities())
                    {

                        shopping_cart s = me.shopping_cart.SingleOrDefault(m => m.product_id == id && m.user_id == user_id);
                        s.quantity = cart[index].Quantity;
                        s.Price = s.Price * cart[index].Quantity;

                        me.SaveChanges();
                        check = true;
                    }
                }
                Session["cart"] = cart;
            }

            if (ModelState.IsValid)
            {
                using (MyEntities db = new MyEntities())
                {
                    if (check == false)
                    {
                        db.shopping_cart.Add(sc);
                        db.SaveChanges();
                    }
                }

            }
            check = false;
            return View("cart");
        }

        

        
        public ActionResult poCart() //generating PO.
        {
            Random random = new Random();
            MyEntities ME = new MyEntities();
            List<shopping_cart> cartItems = new List<shopping_cart>();
            List<po> poItemsList = new List<po>();
            

            cartItems = ME.shopping_cart.ToList(); // getting all the items in the cart.

            var result = cartItems.GroupBy(x => x.SupplierId); // group by supplier id.
            // products added to the cart by same user will go to different supplier -> so different POs.

            foreach(var r in result) // looping through the grouped data.
            {
                po POItems = new po();
                int id = 0;
                id = random.Next(0, 10000); // generating a random id for PO.
                
                
                var u = ME.poes.FirstOrDefault(x => x.PO_ID == id); // checking if the generated id is already present in the list.
                
                while(u != null) // unless the generated ID is not found, keep on generating a new ID and checking.
                {
                    
                    id = random.Next(0, 10000);
                    u = ME.poes.FirstOrDefault(x => x.PO_ID == id);
                }

                POItems.PO_ID = id;
                

                foreach(var cart in r) // now the grouped result is a set of values, so looping through group of products for grouped supplier.
                {
                    POItems.user_id = cart.user_id;
                    POItems.status = "Pending";
                    POItems.product_id = cart.product_id;
                    POItems.SupplierId = cart.SupplierId;
                    POItems.ProductName = cart.ProductName;
                    POItems.ProductDescription = cart.ProductDescription;
                    POItems.Image = cart.Image;
                    POItems.Price = cart.Price;
                    POItems.shopping_date = DateTime.Now;
                    POItems.quantity = cart.quantity;

                    if (ModelState.IsValid)
                    {
                        using (MyEntities db = new MyEntities())
                        {
                            db.poes.Add(POItems);
                            db.SaveChanges(); // adding and saving changes to DB.
                        }

                    }
                }
            }

            return RedirectToAction("po", "PO"); // returning PO view.
        }
        public shopping_cart product(int id) // when clicking on add to cart for a product, it will take the ID of the product and add it to the cart.
        {
            shopping_cart sc = new shopping_cart();

            using (MyEntities me = new MyEntities())
            {
                int user_id = Convert.ToInt32(Session["userId"].ToString()); // getting the User ID to see which user added the product to the cart.
                sc = me.shopping_cart.SingleOrDefault(m => m.product_id == id && m.user_id == user_id);
                // line 192 -> finding the added product in the DB against the user who added that.
            }

            return sc; // returning the shopping_cart object. If already present then only quantity and price will be updated.
        }

        public ActionResult add(List<Products> prod) // ignore this for now.
        {
            prod = new List<Products>();

            if (ModelState.IsValid)
            {
                
                string check = Request.Form["IsSelected"];
            }

            ProductsContext p = new ProductsContext();

            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();

                foreach(Products pr in prod)
                {
                    //if(pr.Selected == true)
                    //{
                    //    cart.Add(new Item(p.Products.Find(pr.Id), 1));
                    //}
                }

                
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                foreach (Products pr in prod)
                {
                    //if (pr.Selected == true)
                    //{
                    //    int index = isPresent(cart, pr.Id);
                    //    if (index == -1)
                    //    {
                    //        cart.Add(new Item(p.Products.Find(pr.Id), 1));
                    //    }
                    //    else
                    //    {
                    //        cart[index].Quantity++;
                    //    }
                    //    Session["cart"] = cart;
                    //}
                }
                
            }
            return View("cart");
        }

        public ActionResult Delete(int id) // deleting product by taking it's id.
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isPresent(cart, id);
            cart.RemoveAt(index);
            Session["cart"] = cart;

            using(MyEntities me = new MyEntities()) // removing that product from DB.
            {
                int user_id = Convert.ToInt32(Session["userId"].ToString()); // deleting the product against the user who added it.
                shopping_cart s = me.shopping_cart.SingleOrDefault(m => m.product_id == id && m.user_id == user_id); // searching in DB.
                me.shopping_cart.Remove(s); // removing.
                me.SaveChanges(); // saving changes to DB.
            }

            return View("cart"); // returning cart view to the view.
        }

        public ActionResult Update(FormCollection fc) // FormCollection will have all the values of HTML elements present on the cart page.
        {
            string[] quantities = fc.GetValues("quantity"); // because fc will have all the values and because it is an HTML page, data type will be string and we store it in an array.

            List<Item> cart = (List<Item>)Session["cart"]; // take the session of the cart.


            for (int i = 0; i < cart.Count; i ++ )
            {
                cart[i].Quantity = Convert.ToInt32(quantities[i]); // converting quantity to integer.

                    
                using (MyEntities me = new MyEntities()) // searching the product against user in DB.
                {
                    int user_id = Convert.ToInt32(Session["userId"].ToString());
                    int id = cart[i].Product.Id;
                    shopping_cart s = me.shopping_cart.SingleOrDefault(m => m.product_id == id && m.user_id == user_id);
                    s.quantity = cart[i].Quantity;
                    s.Price = s.Price * cart[i].Quantity;

                    me.SaveChanges(); // update in DB and saving changes.
                }

            }
            Session["cart"] = cart;

            return View("cart"); // returning cart view.
        }

        public int isPresent(List<Item> cart, int id) // finding if the product is present in the cart. Used in the function above.
        {
            
            
            for(int i = 0; i < cart.Count; i++ )
            {
                if(cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;

        }

    }
}
