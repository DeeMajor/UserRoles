using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class RentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rents
        public ActionResult Index()
        {
            var rents = db.rents.Include(r => r.AdvertBuildings);
            return View(rents.ToList());
        }

        // GET: Rents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rents/Create
        public ActionResult Create()
        {
            ViewBag.BuildDataID = new SelectList(db.AdvertBuilding, "BuildDataID", "BuildingName");
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalId,BuildDataID,Check_In,Check_Out,NumMonths,RoomPrice,TotalPrice,Instalments,FlatDescription,FlatType")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                rent.RoomPrice = rent.GetRoomPrice();
                rent.NumMonths = rent.GetNumberMonths();
                rent.TotalPrice = rent.Calc_Total();
                rent.Instalments = rent.Calc_Installment();
                rent.FlatDescription = rent.GetFlatDescription();
                rent.FlatType = rent.GetBuildType();
                db.rents.Add(rent);
                db.SaveChanges();
                return RedirectToAction("PayFast");
                
            }

            ViewBag.BuildDataID = new SelectList(db.AdvertBuilding, "BuildDataID", "BuildingName", rent.BuildDataID);
            return View(rent);
        }

        // GET: Rents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildDataID = new SelectList(db.AdvertBuilding, "BuildDataID", "BuildingName", rent.BuildDataID);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalId,BuildDataID,Check_In,Check_Out,NumMonths,RoomPrice,TotalPrice,Instalments,FlatDescription,FlatType")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildDataID = new SelectList(db.AdvertBuilding, "BuildDataID", "BuildingName", rent.BuildDataID);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent rent = db.rents.Find(id);
            db.rents.Remove(rent);
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
        public ActionResult PayFast()
        {


           Rent res = new Rent();

            
            res.RoomPrice = res.RoomPrice;

            // Create the order in your DB and get the ID
            double amount = res.RoomPrice;
            string orderID = new Random().Next(1, 9999).ToString(); ;
            string name = $"Reservation Payment | Ref No.: 234";
            string description = "Rental Payment";

            string site = "";
            string merchant_id = "";
            string merchant_key = "";

            // Check if we are using the test or live system
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchant_id = "10000100";
                merchant_key = "46f0cd694581a";
            }
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/process?";
                merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
                merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
            }
            else
            {
                throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
            }
            // Build the query string for payment site

            StringBuilder str = new StringBuilder();
            str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
            str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
            str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
            str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
            //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

            str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderID));
            str.Append("&amount=" + HttpUtility.UrlEncode(amount.ToString()));
            str.Append("&item_name=" + HttpUtility.UrlEncode(name));
            str.Append("&item_description=" + HttpUtility.UrlEncode(description));
            decimal AmountPayed = 99999;
            // MembershipPackage cause = db.MembershipPackages.Find(cid);
            //cause.price += P_amount;
            db.SaveChanges();

            // Redirect to PayFast
            Response.Redirect(site + str.ToString());
            //return (site + str.ToString());

            return View();
        }
    }
}
