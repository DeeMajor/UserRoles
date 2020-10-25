using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.appointments.Include(a => a.agent);
            return View(appointments.ToList());
        }

        public ActionResult Confirm(int? id)
        {
            Appointment appointment = db.appointments.Find(id);
            Confirm_Appointment confirm = new Confirm_Appointment();
            confirm.AppId = appointment.AppId;
            confirm.name = appointment.name;
            confirm.surname = appointment.surname;
            confirm.phone = appointment.phone;
            confirm.Address = appointment.Address;
            confirm.Date = appointment.Date;
            confirm.time = appointment.time;
            confirm.AgentId = appointment.AgentId;

            appointment.status = "Appointment Approved";
            db.ConfirmAppointments.Add(confirm);
            db.appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Confirm_Appointment");
        }
            // GET: Appointments/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppId,name,surname,phone,Address,Date,time,AgentId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                //MailMessage nn = new MailMessage();
                //nn.To.Add(appointment.Address);
                //nn.From = new MailAddress("Kondlozibele@gmail.com");
                //nn.Subject = "Appointment Details";
                //nn.Body = " <b>Your Appointment</b>," + " <b>Address:</b>  " + appointment.Address + "<br/> "
                //  + "<b>Class:</b> " + appointment.Date + "<br/>" +
                //   " <b>Your trainer:</b>  " + appointment.time + "<br/>" +
                //   " <b>Client :</b>  " + "<b>" + appointment.phone + "</b>" + "<br/>" +
                //   "<b>Starting Time:</b> " + appointment.name + "<br/>"
                //     + 

                //   " Thank you," + "<b>" + " For the Appointment.";
                //nn.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.EnableSsl = true;

                //NetworkCredential nc = new NetworkCredential("KondloZibele@gmail.com", "248652Zibele");
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = nc;
                //smtp.Send(nn);
                //ViewBag.Message = "Mail has been sent";
                appointment.status = "Waiting Appointment Approval";
                db.appointments.Add(appointment);
                db.SaveChanges();
                TempData["Alert Message"] = "Wait for Confirmation from Agent.";
                return RedirectToAction("Index","Confirm_Appointment");
            }

            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName", appointment.AgentId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName", appointment.AgentId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppId,name,surname,phone,Address,Date,time,AgentId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.agents, "AgentId", "AgentName", appointment.AgentId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.appointments.Find(id);
            db.appointments.Remove(appointment);
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
