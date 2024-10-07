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
    public class AddtocartsController : Controller
    {
        private readonly OnlineFoodDeliveryEntities db = new OnlineFoodDeliveryEntities();

        // GET: Addtocarts
        public ActionResult Index()
        {
            var addtocarts = db.addtocarts.Include(a => a.product);
            return View(addtocarts.ToList());
        }

        
        public ActionResult Create()
        {
            ViewBag.productid = new SelectList(db.products, "productid", "productname");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartid,productid,quantity,amount")] addtocart addtocart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.addtocarts.Add(addtocart);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.productid = new SelectList(db.products, "productid", "productname", addtocart.productid);
                return View(addtocart);
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
            
        }

        // GET: Addtocarts/Edit/5
        
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                addtocart addtocart = db.addtocarts.Find(id);
                if (addtocart == null)
                {
                    return HttpNotFound();
                }
                return View(addtocart);
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }

        // POST: Addtocarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                addtocart addtocart = db.addtocarts.Find(id);
                db.addtocarts.Remove(addtocart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }
        public ActionResult Index1()
        {
            var cartItems = db.addtocarts.Include("Product").ToList();
            return View(cartItems);
        }

        // Method to add/update cart item
        [HttpPost]
        public ActionResult AddToCart(int productid, int quantity)
        {
            try
            {
                // Fetch the product price
                var product = db.products.Find(productid);
                if (product == null)
                {
                    return HttpNotFound();
                }

                // Calculate total amount
                int totalAmount = (int)(product.price * quantity);

                // Check if item already exists in cart
                var cartItem = db.addtocarts.SingleOrDefault(ci => ci.productid == productid);
                if (cartItem != null)
                {
                    // Update quantity and amount if item exists
                    cartItem.quantity += quantity;

                    cartItem.amount = product.price * cartItem.quantity;
                }
                else if (cartItem.quantity != null)
                {

                    cartItem.quantity -= quantity;
                    cartItem.amount = cartItem.quantity * product.price;
                    db.addtocarts.Remove(cartItem);
                }


                else
                {
                    // Add new item to cart
                    cartItem = new addtocart
                    {
                        productid = productid,
                        quantity = quantity,
                        amount = totalAmount
                    };
                    db.addtocarts.Add(cartItem);
                }

                db.SaveChanges();
                return RedirectToAction("Index"); // Redirect to your cart index or another view
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
        }

        // Optionally, you can add a method to remove items from the cart
        public ActionResult RemoveFromCart(int cartItemId)
        {
            try
            {
                var cartItem = db.addtocarts.Find(cartItemId);
                if (cartItem != null)
                {
                    db.addtocarts.Remove(cartItem);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
            }
            return View();
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
