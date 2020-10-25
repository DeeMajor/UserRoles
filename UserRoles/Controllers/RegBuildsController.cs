using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class RegBuildsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegBuilds
        public ActionResult Index()
        {
            return View(db.Regbuilds.ToList());
        }
        public ActionResult Approved(int? id)
        {
            RegBuild Reg = db.Regbuilds.Find(id);
            AdvertBuildings approved = new AdvertBuildings();
            approved.BuildDataID = Reg.BuildDataID;
            approved.BuildingName = Reg.BuildingName;
            approved.BuildingAddress = Reg.BuildingAddress;
            approved.City = Reg.City;
            approved.ZipCode = Reg.ZipCode;
            approved.BuildType = Reg.BuildType;
            approved.FlatDescription = Reg.FlatDescription;
            approved.FlatPrice = Reg.FlatPrice;
            approved.NumberFlat = Reg.NumberFlat;
            approved.image = Reg.image;
            approved.Image_Name = Reg.Image_Name;
           

            approved.Status = "Building Approved";
            db.AdvertBuilding.Add(approved);
            db.Regbuilds.Remove(Reg);
            db.SaveChanges();
            return RedirectToAction("Index", "AdvertBuildings");



        }

        // GET: RegBuilds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegBuild regBuild = db.Regbuilds.Find(id);
            if (regBuild == null)
            {
                return HttpNotFound();
            }
            return View(regBuild);
        }

        // GET: RegBuilds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegBuilds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name")] RegBuild regBuild, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            regBuild.image = data;
            if (ModelState.IsValid)
            {
                
                regBuild.Status = "Appending Review";
                db.Regbuilds.Add(regBuild);
                db.SaveChanges();
                return RedirectToAction("Index", "AdvertBuildings");
            }
            return View(regBuild);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: RegBuilds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegBuild regBuild = db.Regbuilds.Find(id);
            if (regBuild == null)
            {
                return HttpNotFound();
            }
            return View(regBuild);
        }

        // POST: RegBuilds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name")] RegBuild regBuild, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            regBuild.image = data;
            if (ModelState.IsValid)
            {
                db.Entry(regBuild).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Alert Message"] = "Waiting For Approval From System Admin";
                return RedirectToAction("Index");
            }
            return View(regBuild);
        }

        // GET: RegBuilds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegBuild regBuild = db.Regbuilds.Find(id);
            if (regBuild == null)
            {
                return HttpNotFound();
            }
            return View(regBuild);
        }

        // POST: RegBuilds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegBuild regBuild = db.Regbuilds.Find(id);
            db.Regbuilds.Remove(regBuild);
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
