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
    public class BuildingDatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BuildingDatas
        public ActionResult Index()
        {
            return View(db.buildingDatas.ToList());
        }

        // GET: BuildingDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingData buildingData = db.buildingDatas.Find(id);
            if (buildingData == null)
            {
                return HttpNotFound();
            }
            return View(buildingData);
        }

        // GET: BuildingDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuildingDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice")] BuildingData buildingData)
        {
            if (ModelState.IsValid)
            {
                db.buildingDatas.Add(buildingData);
                db.SaveChanges();
                return RedirectToAction("Create","Images");
            }

            return View(buildingData);
        }

        // GET: BuildingDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingData buildingData = db.buildingDatas.Find(id);
            if (buildingData == null)
            {
                return HttpNotFound();
            }
            return View(buildingData);
        }

        // POST: BuildingDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice")] BuildingData buildingData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buildingData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buildingData);
        }

        // GET: BuildingDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingData buildingData = db.buildingDatas.Find(id);
            if (buildingData == null)
            {
                return HttpNotFound();
            }
            return View(buildingData);
        }

        // POST: BuildingDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuildingData buildingData = db.buildingDatas.Find(id);
            db.buildingDatas.Remove(buildingData);
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
