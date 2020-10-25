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
    public class ResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Results
        public ActionResult Index(string SearchString)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            List<Result> results = new List<Result>();

            var Admin = (from Appointment in db.appointments
                         join zee in db.agents on Appointment.AppId equals zee.AgentId
                         where Appointment.name==SearchString
                         select new { Appointment.name, Appointment.surname, Appointment.phone, zee.AgentName }).ToList();
            foreach (var item in Admin)
            {
                Result AbjAdmin = new Result();
                AbjAdmin.name = item.name;
                AbjAdmin.surname = item.surname;
                AbjAdmin.phone = item.phone;
                AbjAdmin.AgentName = item.AgentName;
                results.Add(AbjAdmin);
            }
            return View(results);
        }

        // GET: Results/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResId,name,surname,phone,AgentName")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(result);
        }

        // GET: Results/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResId,name,surname,phone,AgentName")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(result);
        }

        // GET: Results/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Result result = db.results.Find(id);
            db.results.Remove(result);
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
