using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RR_RoadWays_Services.Models;

namespace RR_RoadWays_Services.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult getDataValidationDetails()
        {
            var context = new RRRoadwaysDBContext();
            var DataValidationDetails = context.DataValidation.Join(context.Vehicle, vo => vo.Vehicle, v => v.Id, (vo, v) => new
            {
                Id = vo.Id,
                Vehicle = v.VehicleNumber,
                Srp = vo.Srp,
                Prp = vo.Prp,
                KpkRp = vo.KpkRp,
                TaxPaper = vo.TaxPaper,
                Fitness = vo.Fitness,
                Insurance = vo.Insurance
            }).ToList().OrderByDescending(x => x.Id);
            return Json(new { data = DataValidationDetails }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            return View(new DataValidation());
        }

        [HttpPost]
        public IActionResult Add(DataValidation data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                context.Add(data);
                context.SaveChanges();
                ViewBag.result = "Record Saved Successfully!";
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
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
            var std = context.DataValidation.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(DataValidation data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("Srp").IsModified = true;
                dbEntry.Property("Prp").IsModified = true;
                dbEntry.Property("KpkRp").IsModified = true;
                dbEntry.Property("TaxPaper").IsModified = true;
                dbEntry.Property("Fitness").IsModified = true;
                dbEntry.Property("Insurance").IsModified = true;

                context.SaveChanges();
                ViewBag.result = "Record Updated Successfully!";
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
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
