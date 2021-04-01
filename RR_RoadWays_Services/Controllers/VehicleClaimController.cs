using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RR_RoadWays_Services.Models;

namespace RR_RoadWays_Services.Controllers
{
    public class VehicleClaimController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var context = new RRRoadwaysDBContext();
            var rRRoadwaysDBContext = context.VehicleClaim.Include(v => v.Vehicle);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getClaims()
        {
            var context = new RRRoadwaysDBContext();
            //var dataClaims= context.VehicleClaim.ToList().OrderByDescending(x => x.Id);
            List<VehicleClaim> claims = context.VehicleClaim.ToList();
            List<Vehicle> vehicles = context.Vehicle.ToList();


            var dataClaims = (from c in claims
                                     join v in vehicles on c.VehicleId equals v.Id
                                     select new { c.Id, v.VehicleNumber, ClaimDate = c.ClaimDate.GetValueOrDefault().ToString("dddd, dd MMMM yyyy"), c.Claim, c.Description, c.Amount }
                                     ).ToList();

            return Json(new { data = dataClaims }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");

            return View(new VehicleClaim());
        }

        [HttpPost]
        public IActionResult Add(VehicleClaim data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                data.EntryDate = DateTime.Now.Date;
                context.Add(data);
                context.SaveChanges();

                ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");
                ViewBag.result = "Record Saved Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.VehicleClaim.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(VehicleClaim data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("ClaimDate").IsModified = true;
                dbEntry.Property("Claim").IsModified = true;
                dbEntry.Property("Description").IsModified = true;
                dbEntry.Property("Amount").IsModified = true;

                context.SaveChanges();
                ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");
                ViewBag.result = "Record Updated Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dataClaim = context.VehicleClaim.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataClaim);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }
    }

}
