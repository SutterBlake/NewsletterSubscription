using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsletterSubscription.DataLayer;
using NewsletterSubscription.Models;

namespace NewsletterSubscription.Controllers
{
    public class SubscriptionsController : Controller
    {
        private SubscriptionDBContext db = new SubscriptionDBContext();

        // GET: Subscriptions
        public ActionResult Index()
        {
            return View(db.Subscriptions.ToList());
        }

        [HttpPost]
        public bool AJAXCheckEmailExists(string email)
        {
            if (db.Subscriptions.Any(s => s.Email.Trim() == email.Trim()))
                return true;
            else
                return false;
        }

        // GET: Subscriptions/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Subscriptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "ID,Email,Source,ReasonSignUp")] Subscription subscriptor)
        {
            if (ModelState.IsValid)
            {
                // We are already checking whether email exists in DB using AJAX, 
                // but server side validation its always a must!
                if (db.Subscriptions.Any(o => o.Email == subscriptor.Email))
                {
                    ViewBag.MessageType = "danger";
                    ViewBag.Message = "Ooops! This email is already registered";

                    return View(subscriptor);
                }
                else
                {
                    ViewBag.MessageType = "info";
                    ViewBag.Message = "Great! You registered to our newsletter";

                    subscriptor.SubscriptionDate = DateTime.Now;
                    db.Subscriptions.Add(subscriptor);
                    db.SaveChanges();
                    ModelState.Clear();
                    return View();
                }
            }

            ViewBag.MessageType = "danger";
            ViewBag.Message = "Error! Please, check all fields has a proper value";

            return View(subscriptor);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWithPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Subscriptions.Remove(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
