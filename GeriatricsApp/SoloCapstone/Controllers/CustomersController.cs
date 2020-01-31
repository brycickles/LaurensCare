using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SoloCapstone.Models;

namespace SoloCapstone.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            //tie to application user table
            string userId = User.Identity.GetUserId();

            var customer = db.Customers.Where(c => c.ApplicationId == userId).FirstOrDefault();

            return View(customer);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CFirstName,CLastName,CEmail,CPhoneNumber,CCity,CStreet,CState,CZipCode,RFirsName,RLastName,RCity,RStreet,RState,RZipCode,ConsultMessage,HasBeenConsulted")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //tie to application user table
                string userId = User.Identity.GetUserId();
                customer.ApplicationId = userId;

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("ConsultationRequest");
            }

            return View(customer);
        }

        public ActionResult ConsultationRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsultationRequest(Customer customer)
        {
            ConsultationRequest ApiConsultPOST = new ConsultationRequest();
            string userId = User.Identity.GetUserId();

            //remember to update database with saved customer
            var customerFromDb = db.Customers.Where(c => c.ApplicationId == userId).FirstOrDefault();
            customer.CCity = customerFromDb.CCity;
            customer.CEmail = customerFromDb.
            customer.CFirstName = customerFromDb.
            customer.CLastName = customerFromDb.
            customer.

            //fill model with relevant data to utilize in POST 
            ApiConsultPOST.ClientId = customer.CustomerId;
            ApiConsultPOST.FirstName = customer.CFirstName;
            ApiConsultPOST.LastName = customer.CLastName;
            ApiConsultPOST.PhoneNumber = customer.CPhoneNumber;
            ApiConsultPOST.Message = customer.ConsultMessage; 


            return View("Index", customer);
        }




        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CFirstName,CLastName,CEmail,CPhoneNumber,CCity,CStreet,CState,CZipCode,RFirsName,RLastName,RCity,RStreet,RState,RZipCode,ConsultMessage,HasBeenConsulted")] Customer customer)
        {
            if (ModelState.IsValid)
            {   db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
