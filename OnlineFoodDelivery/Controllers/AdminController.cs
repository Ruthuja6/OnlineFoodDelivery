using OnlineFoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    public class AdminController : Controller
    {
       // private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}