﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomerDashboard()
        {
            return View();
        }
    }
}