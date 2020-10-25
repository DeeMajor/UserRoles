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
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Images
        public ActionResult Index()
        {
            return View(db.Image.ToList());
        }
        
        public ActionResult GuestDetails()
        {
            var use = db.Users.ToList().Find(p => p.Email == User.Identity.Name);
            var guest = db.Image.Where(p => p.LandEmail == use.Email);
            return View(guest.ToList());
        }
        public ActionResult Approved(int? id)
        {
            Images images = db.Image.Find(id);
            AdvertBuildings approved = new AdvertBuildings();
            approved.BuildDataID = images.BuildDataID;
            approved.BuildingName = images.BuildingName;
            approved.BuildingAddress = images.BuildingAddress;
            approved.City = images.City;
            approved.ZipCode = images.ZipCode;
            approved.BuildType = images.BuildType;
            approved.FlatDescription = images.FlatDescription;
            approved.FlatPrice = images.FlatPrice;
            approved.NumberFlat = images.NumberFlat;
            approved.image = images.image;
            approved.Image_Name = images.Image_Name;
            approved.LandEmail = images.LandEmail;

            approved.Status = "Building Approved";
            db.AdvertBuilding.Add(approved);
            db.Image.Remove(images);
            db.SaveChanges();
            return RedirectToAction("Index","AdvertBuildings");

            

        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Image.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name")] Images images, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                if (filelist != null && filelist.ContentLength > 0)
                {
                    images.image = ConvertToBytes(filelist);

                }
                images.Status = "Appending Review";
                db.Image.Add(images);
                db.SaveChanges();
                return RedirectToAction("Index","AdvertBuildings");
            }

            return View(images);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Image.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildDataID,BuildingName,BuildingAddress,City,ZipCode,BuildType,NumberFlat,FlatDescription,FlatPrice,image,Image_Name")] Images images)
        {
            if (ModelState.IsValid)
            { 
                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.Image.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Images images = db.Image.Find(id);
            db.Image.Remove(images);
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
