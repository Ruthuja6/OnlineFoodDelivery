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
    public class CancelOrderController : Controller
    {
        private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();

        // GET: CancelOrder
        public ActionResult Index()
        {
            return View(db.orders.ToList());
        }
        // GET: CancelOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }

        // POST: CancelOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                order order = db.orders.Find(id);
                db.orders.Remove(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }

    }
}
