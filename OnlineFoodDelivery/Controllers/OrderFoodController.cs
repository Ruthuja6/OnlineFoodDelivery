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
    public class OrderFoodController : Controller
    {
        private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();

        // GET: OrderFood
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.product);
            return View(orders.ToList());
        }

        // GET: OrderFood/Create
        public ActionResult Create()
        {
            ViewBag.productid = new SelectList(db.products, "productid", "productname");
           
            /*decimal totalAmount = addtocart.Sum(c => c.amount ?? 0);
            var orderModel = new order
            {
               amount = totalAmount  // Set total amount from cart items
            };*/
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderid,productid,quantity,amount,DeliveryAddress,paymentmethod,cardnumber,cardholdername,cvv,expirydate")] order order)
        {
            if (ModelState.IsValid)
            {
               /* // Fetch cart items and calculate total amount
                // Calculate total amount
                decimal totalAmount = addtocart.Sum(c => c.amount ?? 0);

                // Populate order details
               // order.UserId = user.Id;
                order.TotalAmount = totalAmount;  // Use the calculated total from cart*/
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your cart is empty.");
            }

            ViewBag.productid = new SelectList(db.products, "productid", "productname", order.productid);
            return View(order);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrderFood/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
