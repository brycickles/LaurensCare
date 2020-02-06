using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SoloCapstone.Models;

namespace SoloCapstone.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region calendar Action/Json Results
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var events = db.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpPost]
        public ActionResult SaveEvent(Event e)
        {
            bool status = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if(e.EventId > 0)
                {
                    //update the event since the event has already been created
                    var v = db.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if(v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                        v.JournalEntry = e.JournalEntry;
                        v.CustomerId = e.CustomerId;
                    }
                }
                else
                {
                    string userId = User.Identity.GetUserId();
                    e.EmpApplicationId = userId; //set event application id = employee app id to display events specific to employee
                    db.Events.Add(e);
                }
                db.SaveChanges();
                status = true; 
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEvent(int eventId)
        {
            bool status = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var e = db.Events.Where(a => a.EventId == eventId).FirstOrDefault();
                if(e != null)
                {
                    db.Events.Remove(e);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        #endregion

        #region consultation actions
        public ActionResult Consulted(Customer customer)
        {
            var cust = db.Customers.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();
            cust.HasBeenConsulted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RequestMsg(Customer customer)
        {
            return View(customer);            
        }

        public ActionResult AddToCalendar(Customer customer)
        {
            Event newEvent = new Event();
            string userId = User.Identity.GetUserId();
            newEvent.EmpApplicationId = userId;

            newEvent.Subject = "Consultation Request: " + customer.CLastName;
            string currMonthMM = DateTime.Now.ToString("MM");
            string currYearYYYY = DateTime.Now.Year.ToString();
            string inputDate = "01/" + currMonthMM + "/" + currYearYYYY + " 1:00 PM";
            DateTime dt = DateTime.Parse(inputDate);
            newEvent.Start = dt;
            newEvent.End = null;
            newEvent.IsFullDay = true;
            newEvent.ThemeColor = "Blue";
            newEvent.Description = "This is a scheduled consultation request for " + customer.CFirstName + " " + customer.CLastName + " at " + customer.CStreet + ", " + customer.CCity + ", " + customer.CState + "\nPhone Number: " + customer.CPhoneNumber ;

            db.Events.Add(newEvent);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult AddFacilityReview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFacilityReview(Facility facility)
        {
            db.Facilities.Add(facility);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Employees
        public ActionResult Index() //use this index for employee calendar 
        {            
            string userId = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationId == userId).FirstOrDefault();
            ViewBag.Name = employee.FirstName + " " + employee.LastName;
            ViewBag.EmployeeId = userId;

            //take list of customers that have not been consulted and display it in a view to employees index page to review (reference trash collector to get this one.)           
            var CustomersToBeConsulted = db.Customers.Where(c => c.HasBeenConsulted == false).ToList();
            List<Customer> ConsultationList = CustomersToBeConsulted;

            ViewData["Employees"] = employee;
            ViewData["Customers"] = ConsultationList;

            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,Email,PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //tie employee to user table
                string userId = User.Identity.GetUserId();
                employee.ApplicationId = userId;

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,Email,PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
