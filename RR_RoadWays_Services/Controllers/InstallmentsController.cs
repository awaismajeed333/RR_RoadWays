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
    public class InstallmentsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var context = new RRRoadwaysDBContext();
            var rRRoadwaysDBContext = context.Installment.Include(v => v.Vehicle);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getInstallments()
        {
            var context = new RRRoadwaysDBContext();
            //var dataInstallments = context.Installment.ToList().OrderByDescending(x => x.Id);
            List<Installment> installments = context.Installment.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            var dataInstallments = (from i in installments
                                    join v in vehicles on i.VehicleId equals v.Id
                                            select new { i.Id, v.VehicleNumber, InstallmentsMonth = mfi.GetMonthName(Convert.ToInt32(i.InstallmentsMonth)), InstallmentDate = i.InstallmentDate.GetValueOrDefault().ToString("dddd, dd MMMM yyyy"),i.InstallmentDetail, i.Amount}
                                     ).ToList();

            
            return Json(new { data = dataInstallments }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");

            return View(new Installment());
        }

        [HttpPost]
        public IActionResult Add(Installment data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                data.EntryDate = DateTime.Now.Date;
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
            var std = context.Installment.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
          
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Installment data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("VehicleId").IsModified = true;
                dbEntry.Property("InstallmentsMonth").IsModified = true;
                dbEntry.Property("InstallmentDate").IsModified = true;
                dbEntry.Property("InstallmentDetail").IsModified = true;
                dbEntry.Property("Amount").IsModified = true;

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
                var dataInstallment = context.Installment.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataInstallment);
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