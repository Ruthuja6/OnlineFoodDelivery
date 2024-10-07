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
    public class AvailableFoodItemsController : Controller
    {
        private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();

        // GET: AvailableFoodItems
        public ActionResult Index()
        {
            return View(db.products.ToList());
        }

        // GET: AvailableFoodItems/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                product product = db.products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }
    }
}
