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
    public class FixedExpansesController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var context = new RRRoadwaysDBContext();
            var rRRoadwaysDBContext = context.FixedExpanse.Include(v => v.Vehicle);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getFixedExpanses()
        {
            var context = new RRRoadwaysDBContext();
            //var dataFixedExpanses = context.FixedExpanse.ToList().OrderByDescending(x => x.Id);

            List<FixedExpanse> fixedExpanses = context.FixedExpanse.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            var dataFixedExpanses = (from f in fixedExpanses
                                    join v in vehicles on f.VehicleId equals v.Id
                                    select new { f.Id, v.VehicleNumber, ExpanseMonth = mfi.GetMonthName(Convert.ToInt32(f.ExpanseMonth)), f.StaffSalary,f.StaffBhatta,f.Donation,f.DriverRoomRent,f.FormanSalary,f.ExtraDriversSalary,f.ExtraExpense }
                                     ).ToList();
            return Json(new { data = dataFixedExpanses}, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
           
            return View(new FixedExpanse());
        }

        [HttpPost]
        public IActionResult Add(FixedExpanse data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                context.Add(data);
                context.SaveChanges();

                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
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
            var std = context.FixedExpanse.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(FixedExpanse data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("StaffSalary").IsModified = true;
                dbEntry.Property("StaffBhatta").IsModified = true;
                dbEntry.Property("BhattaDetails").IsModified = true;
                dbEntry.Property("Donation").IsModified = true;
                dbEntry.Property("DriverRoomRent").IsModified = true;
                dbEntry.Property("FormanSalary").IsModified = true;
                dbEntry.Property("ExtraDriversSalary").IsModified = true;
                dbEntry.Property("ExtraExpense").IsModified = true;
                dbEntry.Property("ExtraExpenseDetails").IsModified = true;
                
                context.SaveChanges();
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
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
                var dataFixedExpanse = context.FixedExpanse.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataFixedExpanse);
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
