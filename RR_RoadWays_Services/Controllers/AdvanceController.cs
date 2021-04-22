using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RR_RoadWays_Services.Models;

namespace RR_RoadWays_Services.Controllers
{
    public class AdvanceController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var context = new RRRoadwaysDBContext();
            var rRRoadwaysDBContext = context.Advance.Include(v => v.Vehicle).Include(s=>s.Station);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getAdvance()
        {
            var context = new RRRoadwaysDBContext();

            List<Advance> Advances = context.Advance.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            List<Station> stations = context.Station.ToList();


            //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            var dataAdvances = (from a in Advances
                                    join v in vehicles on a.VehicleId equals v.Id join s in stations on a.StationId equals s.Id
                                    select new { a.Id, v.VehicleNumber, s.Name, a.AdvanceDate, a.Description, a.Amount }
                                     ).ToList();

            
            return Json(new { data = dataAdvances }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");

            return View(new Advance());
        }

        [HttpPost]
        public IActionResult Add(Advance data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                context.Add(data);
                context.SaveChanges();
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");
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
            var std = context.Advance.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Advance data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("VehicleId").IsModified = true;
                dbEntry.Property("StationId").IsModified = true;
                dbEntry.Property("AdvanceDate").IsModified = true;
                dbEntry.Property("Description").IsModified = true;
                dbEntry.Property("Amount").IsModified = true;

                context.SaveChanges();
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");

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
                var dataAdvance = context.Advance.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataAdvance);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveVehicle(Vehicle data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                data.CreatedDate = DateTime.Now.Date;
                data.IsDeleted = false;
                context.Add(data);
                context.SaveChanges();
                ViewBag.result = "Record Saved Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            ModelState.Clear();
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }

}