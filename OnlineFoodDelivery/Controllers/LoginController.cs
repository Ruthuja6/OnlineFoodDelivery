using OnlineFoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    public class LoginController : Controller
    {
        private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();
        public ActionResult LoginPage ()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the admin exists in the database
                    var existingAdmin = db.admins.FirstOrDefault(a => a.username == admin.username && a.password == admin.password);

                    if (existingAdmin != null)
                    {
                        // Successfully logged in, redirect to Admin dashboard
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else
                    {
                        // If the credentials are incorrect
                        ViewBag.ErrorMessage = "Invalid username and password";
                        return View(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            // If the model state is not valid, return to the login view
            return View(admin);
        }

        //userlogin
        public ActionResult CustomerLogin() => View();
        [HttpPost]
        public ActionResult CustomerLogin(string username, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = db.customers.SingleOrDefault(u => u.username == username && u.password == password);
                    if (user != null)
                    {
                        Session["customerid"] = user.customerid;
                        return RedirectToAction("CustomerDashboard", "Customer");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid login credentials";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }
        public ActionResult CustomerRegister() => View();

        [HttpPost]
        public ActionResult CustomerRegister(customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("CustomerLogin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View(customer);
        }
    }
}