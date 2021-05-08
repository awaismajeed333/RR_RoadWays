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
            var rRRoadwaysDBContext = context.Advance.Include(v => v.Vehicle);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getAdvance()
        {
            var context = new RRRoadwaysDBContext();

            List<Advance> Advances = context.Advance.ToList();
            List<AdvanceDetails> AdvanceDetails = context.AdvanceDetails.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            List<Station> stations = context.Station.ToList();

            var dataAdvances = (from a in Advances join ad in AdvanceDetails on a.Id equals ad.AdvanceId
                                join v in vehicles on a.VehicleId equals v.Id
                                join s in stations on ad.StationId equals s.Id
                                select new { a.Id, v.VehicleNumber, s.Name, ad.AdvanceDate, ad.Description, ad.Amount }
                                   ).ToList();

            //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            //var dataAdvances = (from a in Advances
            //                        join v in vehicles on a.VehicleId equals v.Id join s in stations on a.StationId equals s.Id
            //                        select new { a.Id, v.VehicleNumber, s.Name, a.AdvanceDate, a.Description, a.Amount }
            //                         ).ToList();


            return Json(new { data = dataAdvances }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public ActionResult Details(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.Advance.Where(a => a.Id == Id).Include(ad => ad.AdvanceDetails).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");
            ViewBag.StationType = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Pump", Value = "Pump"},
                    new SelectListItem { Selected = false, Text = "Oil Shop", Value = "OilShop"},
                    new SelectListItem { Selected = false, Text = "Maintenance Shop", Value = "MaintenanceShop"},
                    new SelectListItem { Selected = false, Text = "Pickup Point", Value = "PickupPoint"},
                }, "Value", "Text");
            ViewBag.cities = new SelectList(context.City.ToList(), "Id", "Name");

            return View(std);
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");

            ViewBag.StationType = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Pump", Value = "Pump"},
                    new SelectListItem { Selected = false, Text = "Oil Shop", Value = "OilShop"},
                    new SelectListItem { Selected = false, Text = "Maintenance Shop", Value = "MaintenanceShop"},
                    new SelectListItem { Selected = false, Text = "Pickup Point", Value = "PickupPoint"},
                }, "Value", "Text");
            ViewBag.cities = new SelectList(context.City.ToList(), "Id", "Name");

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

                ViewBag.StationType = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Pump", Value = "Pump"},
                    new SelectListItem { Selected = false, Text = "Oil Shop", Value = "OilShop"},
                    new SelectListItem { Selected = false, Text = "Maintenance Shop", Value = "MaintenanceShop"},
                    new SelectListItem { Selected = false, Text = "Pickup Point", Value = "PickupPoint"},
                }, "Value", "Text");
                ViewBag.cities = new SelectList(context.City.ToList(), "Id", "Name");
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
            //var std = context.Advance.Where(s => s.Id == Id).FirstOrDefault();


            var std = context.Advance.Where(a => a.Id == Id)
        .Include(ad => ad.AdvanceDetails).FirstOrDefault();


            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");
            ViewBag.StationType = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Pump", Value = "Pump"},
                    new SelectListItem { Selected = false, Text = "Oil Shop", Value = "OilShop"},
                    new SelectListItem { Selected = false, Text = "Maintenance Shop", Value = "MaintenanceShop"},
                    new SelectListItem { Selected = false, Text = "Pickup Point", Value = "PickupPoint"},
                }, "Value", "Text");
            ViewBag.cities = new SelectList(context.City.ToList(), "Id", "Name");

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Advance data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
                //dbEntry.Property("VehicleId").IsModified = true;
                //dbEntry.Property("StationId").IsModified = true;
                //dbEntry.Property("AdvanceDate").IsModified = true;
                //dbEntry.Property("Description").IsModified = true;
                //dbEntry.Property("Amount").IsModified = true;

                List<int> previousAdvanceDetailIds = context.AdvanceDetails
                          .Where(ad => ad.AdvanceId == data.Id)
                          .Select(ep => ep.Id)
                          .ToList();


                List<int> currentAdvanceDetailIds = data.AdvanceDetails.Select(o => o.Id).ToList();

                List<int> deletedAdvanceDetailIds = previousAdvanceDetailIds.Except(currentAdvanceDetailIds).ToList();

                foreach (var AdvanceDetail in data.AdvanceDetails)
                {
                    if (AdvanceDetail.Id == 0)
                    {
                        context.Entry(AdvanceDetail).State = EntityState.Added;
                        AdvanceDetail.AdvanceId = data.Id;
                    }
                    else
                    {

                        context.Entry(AdvanceDetail).State = EntityState.Modified;
                        AdvanceDetail.AdvanceId = data.Id;
                    }
                }
                foreach (var deletedAdvanceDetailId in deletedAdvanceDetailIds)
                {
                    AdvanceDetails deletedAdvanceDetail = context.AdvanceDetails.Where(ad => ad.AdvanceId == data.Id && ad.Id == deletedAdvanceDetailId).Single();
                    context.Entry(deletedAdvanceDetail).State = EntityState.Deleted;
                }
                context.SaveChanges();

                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.StationId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList(), "Id", "Name");
                ViewBag.StationType = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Pump", Value = "Pump"},
                    new SelectListItem { Selected = false, Text = "Oil Shop", Value = "OilShop"},
                    new SelectListItem { Selected = false, Text = "Maintenance Shop", Value = "MaintenanceShop"},
                    new SelectListItem { Selected = false, Text = "Pickup Point", Value = "PickupPoint"},
                }, "Value", "Text");
                ViewBag.cities = new SelectList(context.City.ToList(), "Id", "Name");

                ViewBag.result = "Record Updated Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return Json(ViewBag.result, new Newtonsoft.Json.JsonSerializerSettings());
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

        [HttpPost]
        public IActionResult SavePickup(Station data)
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

        [HttpPost]
        public ActionResult SaveCashAdvance([FromBody] Advance data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                context.Add(data);
                context.SaveChanges();
                //ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");
                ViewBag.result = "Record Saved Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = "0**" + e.Message;
            }
            ModelState.Clear();
            return Json(ViewBag.result, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }

}