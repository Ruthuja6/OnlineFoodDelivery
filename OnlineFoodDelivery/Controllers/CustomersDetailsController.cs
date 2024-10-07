using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFoodDelivery.Models;

namespace OnlineFoodDelivery.Controllers
{
    public class CustomersDetailsController : Controller
    {
        private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();

        // GET: CustomersDetails
        public ActionResult Index()
        {
            return View(db.customers.ToList());
        }

        // GET: CustomersDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
    }
}
