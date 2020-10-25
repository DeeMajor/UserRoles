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
    public class BuildingAppicationVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BuildingAppicationVMs
        public ActionResult Index()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            List<BuildingAppicationVM> buildingAppicationVMs = new List<BuildingAppicationVM>();

            var Admin = (from BuildingData in db.buildingDatas
                         join KUHLE in db.Images on BuildingData.BuildDataID equals KUHLE.BuildDataID
                         select new { BuildingData.BuildingName, BuildingData.BuildingAddress, BuildingData.City, BuildingData.ZipCode, BuildingData.BuildType, BuildingData.NumberFlat, BuildingData.FlatDescription, BuildingData.FlatPrice, KUHLE.Image_Name, KUHLE.Amount, KUHLE.image }).ToList();
            foreach(var item in Admin)
            {
                BuildingAppicationVM ObjAdmin = new BuildingAppicationVM();
                ObjAdmin.BuildingName = item.BuildingName;
                ObjAdmin.BuildingAddress = item.BuildingAddress;
                ObjAdmin.City = item.City;
                ObjAdmin.ZipCode = item.ZipCode;
                ObjAdmin.BuildType = item.BuildType;
                ObjAdmin.NumberFlat = item.NumberFlat;
                ObjAdmin.FlatDescription = item.FlatDescription;
                ObjAdmin.FlatPrice = item.FlatPrice;
                ObjAdmin.image = item.image;
                ObjAdmin.Image_Name = item.Image_Name;
                ObjAdmin.Amount = item.Amount;
                db.BuildingAppicationVMs.Add(ObjAdmin);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(buildingAppicationVMs);
        }

        // GET: BuildingAppicationVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingAppicationVM buildingAppicationVM = db.BuildingAppicationVMs.Find(id);
            if (buildingAppicationVM == null)
            {
                return HttpNotFound();
            }
            return View(buildingAppicationVM);
        }

        // GET: BuildingAppicationVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuildingAppicationVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingAppicationVM_Id,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,Image,Image_Name,Amount")] BuildingAppicationVM buildingAppicationVM)
        {
            if (ModelState.IsValid)
            {
                db.BuildingAppicationVMs.Add(buildingAppicationVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(buildingAppicationVM);
        }

        // GET: BuildingAppicationVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingAppicationVM buildingAppicationVM = db.BuildingAppicationVMs.Find(id);
            if (buildingAppicationVM == null)
            {
                return HttpNotFound();
            }
            return View(buildingAppicationVM);
        }

        // POST: BuildingAppicationVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingAppicationVM_Id,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,Image,Image_Name,Amount")] BuildingAppicationVM buildingAppicationVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buildingAppicationVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buildingAppicationVM);
        }

        // GET: BuildingAppicationVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingAppicationVM buildingAppicationVM = db.BuildingAppicationVMs.Find(id);
            if (buildingAppicationVM == null)
            {
                return HttpNotFound();
            }
            return View(buildingAppicationVM);
        }

        // POST: BuildingAppicationVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuildingAppicationVM buildingAppicationVM = db.BuildingAppicationVMs.Find(id);
            db.BuildingAppicationVMs.Remove(buildingAppicationVM);
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
