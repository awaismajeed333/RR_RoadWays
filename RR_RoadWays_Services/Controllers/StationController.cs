using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RR_RoadWays_Services.Models;

namespace RR_RoadWays_Services.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class StationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getStations()
        {
            var context = new RRRoadwaysDBContext();
            var dataStations = context.Station.Join(context.City,
                c => c.CityId,
                oa => oa.Id,
                (c, oa) => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    OwnerName = c.OwnerName,
                    ContactNumber = c.ContactNumber,
                    StationType = c.StationType,
                    City = oa.Name,
                    IsDeleted = c.IsDeleted,
                }).Where(c => c.IsDeleted == false).ToList().OrderByDescending(x => x.Id);
            return Json(new { data = dataStations }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";
            ViewBag.stationtypes = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Pump", Value = "Pump"},
                new SelectListItem { Selected = false, Text = "Oil Shop", Value = "OilShop"},
                    new SelectListItem { Selected = false, Text = "Maintenance Shop", Value = "MaintenanceShop"},
                    new SelectListItem { Selected = false, Text = "Pickup Point", Value = "PickupPoint"},


            }, "Value", "Text");
            ViewBag.cities = new SelectList(context.City.ToList(), "Id", "Name");
            return View(new Station());
        }

        [HttpPost]
        public IActionResult Add(Station data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                data.CreatedDate = DateTime.Now.Date;
                data.IsDeleted = false;
                context.Add(data);
                context.SaveChanges();
                ViewBag.stationtypes = new SelectList(new List<SelectListItem>
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
            var std = context.Station.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.stationtypes = new SelectList(new List<SelectListItem>
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
        public IActionResult Edit(Station data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("Name").IsModified = true;
                dbEntry.Property("Location").IsModified = true;
                dbEntry.Property("OwnerName").IsModified = true;
                dbEntry.Property("ContactNumber").IsModified = true;
                dbEntry.Property("StationType").IsModified = true;
                dbEntry.Property("CityId").IsModified = true;


                context.SaveChanges();
                ViewBag.stationtypes = new SelectList(new List<SelectListItem>
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dataStation = context.Station.Where(c => c.Id == id).FirstOrDefault();
                dataStation.IsDeleted = true;
                context.Update(dataStation);
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
