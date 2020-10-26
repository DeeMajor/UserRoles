using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class ApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.ApplicationStatus);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.ApplicationStatus, "StatusID", "Application_status");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                //add the pending status
                application.StatusID = db.ApplicationStatus.Where(x => x.Application_status == "Pending").Select(x => x.StatusID).FirstOrDefault();
                //add the clientId add to the application
                application.clientId = User.Identity.GetUserId();
                db.Applications.Add(application);
                db.SaveChanges();
                //redirect to CustIndex since only they can create an application I assume. If not please change.
                return RedirectToAction("CustIndex");
            }

            ViewBag.StatusID = new SelectList(db.ApplicationStatus, "StatusID", "Application_status", application.StatusID);
            return View(application);
        }
        //admin can approve and change status
        public ActionResult Approve(int? id)
        {
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            application.StatusID = db.ApplicationStatus.Where(x => x.Application_status == "Approved").Select(x => x.StatusID).FirstOrDefault();
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //reject
        public ActionResult Reject(int? id)
        {
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            application.StatusID = db.ApplicationStatus.Where(x => x.Application_status == "Rejected").Select(x => x.StatusID).FirstOrDefault();
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //ClientIndex showing their specific application(s)
        public ActionResult CustIndex()
        {
            var user = User.Identity.GetUserId();
            var applications = db.Applications.Include(a => a.ApplicationStatus);
            return View(applications.Where(x=>x.clientId==user).ToList());
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.ApplicationStatus, "StatusID", "Application_status", application.StatusID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.ApplicationStatus, "StatusID", "Application_status", application.StatusID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
