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
    public class Confirm_AppointmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Confirm_Appointment
        public ActionResult Index()
        {
            var confirmAppointments = db.ConfirmAppointments.Include(c => c.agent);
            return View(confirmAppointments.ToList());
        }

        // GET: Confirm_Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirm_Appointment confirm_Appointment = db.ConfirmAppointments.Find(id);
            if (confirm_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(confirm_Appointment);
        }

        // GET: Confirm_Appointment/Create
        public ActionResult Create()
        {
            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName");
            return View();
        }

        // POST: Confirm_Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppId,name,surname,phone,Address,Date,time,AgentId")] Confirm_Appointment confirm_Appointment)
        {
            if (ModelState.IsValid)
            {
                db.ConfirmAppointments.Add(confirm_Appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName", confirm_Appointment.AgentId);
            return View(confirm_Appointment);
        }

        // GET: Confirm_Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirm_Appointment confirm_Appointment = db.ConfirmAppointments.Find(id);
            if (confirm_Appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName", confirm_Appointment.AgentId);
            return View(confirm_Appointment);
        }

        // POST: Confirm_Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppId,name,surname,phone,Address,Date,time,AgentId")] Confirm_Appointment confirm_Appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confirm_Appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName", confirm_Appointment.AgentId);
            return View(confirm_Appointment);
        }

        // GET: Confirm_Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirm_Appointment confirm_Appointment = db.ConfirmAppointments.Find(id);
            if (confirm_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(confirm_Appointment);
        }

        // POST: Confirm_Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Confirm_Appointment confirm_Appointment = db.ConfirmAppointments.Find(id);
            db.ConfirmAppointments.Remove(confirm_Appointment);
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
