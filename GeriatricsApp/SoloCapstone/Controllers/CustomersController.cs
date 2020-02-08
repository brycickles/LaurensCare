using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
            Customer cmodel = customer; 

            //if customer has not yet been consulted, load customer consulting page to let them know their account creation has been paused until an employee has consulted them. 
            if(cmodel.HasBeenConsulted != true)
            {
                return RedirectToAction("NotYetConsulted", customer);
            }
            else
            {
                //customer has been consulted
                return View(cmodel);
            }            
        }

        public ActionResult CustomerJournals()
        {
            string userId = User.Identity.GetUserId();
            Customer cust = db.Customers.Where(c => c.ApplicationId == userId).FirstOrDefault();
            string custId = Convert.ToString(cust.CustomerId);
            List<Event> journalsByCustomer = db.Events.Where(e => e.CustomerId == custId).ToList();
            return View(journalsByCustomer);
        }
        public ActionResult ViewJournal(Event e){
            return View(e);
        }
        public ActionResult Directions(Facility facility)
        {
            return View(facility);
        }

        public ActionResult WriteReview()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult WriteReview(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FacilityReviews()
        {
            List<Facility> facility = db.Facilities.ToList();
            
            return View(facility);
        }
        public ActionResult NotYetConsulted(Customer customer)
        {
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

            //customer returned object only has property of Consult Message not null
            var customerFromDb = db.Customers.Where(c => c.ApplicationId == userId).FirstOrDefault();
            customerFromDb.ConsultMessage = customer.ConsultMessage;
            db.SaveChanges();

            //fill model with relevant data to utilize in POST 
            ApiConsultPOST.CustomerId = customerFromDb.CustomerId;
            ApiConsultPOST.FirstName = customerFromDb.CFirstName;
            ApiConsultPOST.LastName = customerFromDb.CLastName;
            ApiConsultPOST.PhoneNumber = customerFromDb.CPhoneNumber;
            ApiConsultPOST.ConsultingMessage = customer.ConsultMessage;

            using (var client = new HttpClient())
            {
                //now ApiConsultPOST object is ready with correct data to be posted to API 
                client.BaseAddress = new Uri("https://localhost:44397/api/ClientRequests");

                string json = JsonConvert.SerializeObject(ApiConsultPOST);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("ClientRequests", content);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {                    
                    return RedirectToAction("Index"); //take clients to a processing page where their 
                }  
            }    
            return RedirectToAction("Index");
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
