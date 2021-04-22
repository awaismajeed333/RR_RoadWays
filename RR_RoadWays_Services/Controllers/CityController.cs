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
    public class CityController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getCities()
        {
            var context = new RRRoadwaysDBContext();
            var dataCities = context.City.ToList().OrderByDescending(x => x.Id);
            return Json(new { data = dataCities }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new City());
        }

        [HttpPost]
        public IActionResult Add(City data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
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
            var std = context.City.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(City data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("Name").IsModified = true;
                

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

        public ActionResult Details(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.City.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dataCity = context.City.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataCity);
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