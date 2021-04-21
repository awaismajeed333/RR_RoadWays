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
    public class MaintenancesController : Controller
    {
        // GET: Maintenances
        public async Task<IActionResult> Index()
        {
            var context = new RRRoadwaysDBContext();
            var rRRoadwaysDBContext = context.Maintenance.Include(m => m.Department).Include(m => m.Station).Include(m => m.Vehicle);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getMaintenance()
        {
            var context = new RRRoadwaysDBContext();
           // var dataMaintenance = context.Maintenance.ToList().OrderByDescending(x => x.Id);

            List<Maintenance> maintenances = context.Maintenance.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            List<Department> departments = context.Department.ToList();
            List<Station> stations = context.Station.ToList();

          
            var maintenanceRecord = (from m in maintenances
                                     join v in vehicles on m.VehicleId equals v.Id
                                     join d in departments on m.DepartmentId equals d.Id
                                     join s in stations on m.StationId equals s.Id
                                     select new {m.Id, v.VehicleNumber, MaintenanceDate = m.MaintenanceDate.GetValueOrDefault().ToString("dddd, dd MMMM yyyy"), s.Name, d.DepartmentName, m.Description,m.MaintenanceDesc, m.Amount }
                                     ).ToList();
            
            //return View(maintenanceRecord);

            return Json(new { data = maintenanceRecord }, new Newtonsoft.Json.JsonSerializerSettings());
        }


        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.stationId = new SelectList(context.Station.Where(x => x.StationType == "MaintenanceShop").ToList(), "Id", "Name");
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.departId = new SelectList(context.Department.ToList(), "Id", "DepartmentName");

            return View(new Maintenance());
        }

        [HttpPost]
        public IActionResult Add(Maintenance data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                data.EntryDate = DateTime.Now.Date;
                context.Add(data);
                context.SaveChanges();

                ViewBag.stationId = new SelectList(context.Station.Where(x => x.StationType == "MaintenanceShop").ToList(), "Id", "Name");
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.departId = new SelectList(context.Department.ToList(), "Id", "DepartmentName");
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
            var std = context.Maintenance.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.stationId = new SelectList(context.Station.Where(x => x.StationType == "MaintenanceShop").ToList(), "Id", "Name");
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.departId = new SelectList(context.Department.ToList(), "Id", "DepartmentName");
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Maintenance data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("StationId").IsModified = true;
                dbEntry.Property("DepartmentId").IsModified = true;
                dbEntry.Property("MaintenanceDate").IsModified = true;
                dbEntry.Property("Description").IsModified = true;
                dbEntry.Property("MaintenanceDesc").IsModified = true;
                dbEntry.Property("Amount").IsModified = true;

                context.SaveChanges();
                ViewBag.stationId = new SelectList(context.Station.Where(x => x.StationType == "MaintenanceShop").ToList(), "Id", "Name");
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.departId = new SelectList(context.Department.ToList(), "Id", "DepartmentName");
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
                var dataStation = context.Maintenance.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataStation);
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
        public IActionResult SaveDepartment(Department data)
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
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
