using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RR_RoadWays_Services.Models;

namespace RR_RoadWays_Services.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class VehicleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getVehicles()
        {
            var context = new RRRoadwaysDBContext();
            var dataVehicles = context.Vehicle.Where(c => c.IsDeleted == false).ToList().OrderByDescending(x => x.Id);
            return Json(new { data = dataVehicles }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new Vehicle());
        }

        [HttpPost]
        public IActionResult Add(Vehicle data)
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.Vehicle.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Vehicle data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("VehicleNumber").IsModified = true;
                dbEntry.Property("OwnerName").IsModified = true;
                dbEntry.Property("OwnerContactNumber").IsModified = true;

                context.SaveChanges();
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
                var dataCompany = context.Vehicle.Where(c => c.Id == id).FirstOrDefault();
                dataCompany.IsDeleted = true;
                context.Update(dataCompany);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.Vehicle.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
    }
}