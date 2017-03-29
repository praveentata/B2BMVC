using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using B2B.Models;

namespace B2B.Controllers
{
    public class LoginController : Controller
    {
        
        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Register(UserRegistration user) // passing UserRegistration object which will have all the details used for registration.
        {
            if(ModelState.IsValid)
            {
                using(MyEntities db = new MyEntities())
                {
                    db.UserRegistrations.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Congratulations" +  user.first_name + " " + user.last_name + " . You registration is successful.";
                
            }
            else
            {
                return View();
            }


            return View("Login"); // upon successful registration, returning login view.
           
        }

        


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost] // login action. Data will be posted.
        public ActionResult Login(UserRegistration user)
        {
            using(MyEntities db = new MyEntities())
            {
                // finding details of user trying to login.
                var usr = db.UserRegistrations.Where(u => u.user_name == user.user_name && u.password == user.password).FirstOrDefault();

                if(usr != null && usr.user_role == "buyer")
                {
                    Session["username"] = usr.user_name.ToString(); // Session will store the values of username and id. This is used everywhere to retrieve the logged in user details.
                    Session["userId"] = usr.ID.ToString();

                    return RedirectToAction("products", "Products");
                }
                else if (usr != null && usr.user_role == "supplier")
                {
                    Session["username"] = usr.user_name.ToString();
                    Session["userId"] = usr.ID.ToString();

                    return RedirectToAction("Invoice", "PO");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password does not match");
                }
            }

            return View();
        }

    }
}
