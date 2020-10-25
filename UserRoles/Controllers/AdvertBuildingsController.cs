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
    public class AdvertBuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdvertBuildings
        public ActionResult Index()
        {
            var advertbuilding = db.AdvertBuilding.Include(c => c.image);
            return View(db.AdvertBuilding.ToList());
        }
        public ActionResult GuestDetails()
        {
            var use = db.Users.ToList().Find(p => p.Email == User.Identity.Name);
            var guest = db.AdvertBuilding.Where(p => p.LandEmail == use.Email);
            return View(guest.ToList());
        }

        // GET: AdvertBuildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvertBuildings advertBuildings = db.AdvertBuilding.Find(id);
            if (advertBuildings == null)
            {
                return HttpNotFound();
            }
            return View(advertBuildings);
        }

        // GET: AdvertBuildings/Create
        public ActionResult Create()
        {
            ViewBag.BuildDataID = new SelectList(db.Image, "BuildDataID", "BuildDataID");
            return View();
        }

        // POST: AdvertBuildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name,Status,LandEmail")] AdvertBuildings advertBuildings)
        {
            if (ModelState.IsValid)
            {
                db.AdvertBuilding.Add(advertBuildings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildDataID = new SelectList(db.Image, "BuildDataID", "BuildDataID", advertBuildings.BuildDataID);
            return View(advertBuildings);
        }

        // GET: AdvertBuildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvertBuildings advertBuildings = db.AdvertBuilding.Find(id);
            if (advertBuildings == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildDataID = new SelectList(db.Image, "BuildDataID", "BuildDataID", advertBuildings.BuildDataID);
            return View(advertBuildings);
        }

        // POST: AdvertBuildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name,Status,LandEmail")] AdvertBuildings advertBuildings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertBuildings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildDataID = new SelectList(db.Image, "BuildDataID", "BuildDataID", advertBuildings.BuildDataID);
            return View(advertBuildings);
        }

        // GET: AdvertBuildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvertBuildings advertBuildings = db.AdvertBuilding.Find(id);
            if (advertBuildings == null)
            {
                return HttpNotFound();
            }
            return View(advertBuildings);
        }

        // POST: AdvertBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdvertBuildings advertBuildings = db.AdvertBuilding.Find(id);
            db.AdvertBuilding.Remove(advertBuildings);
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
