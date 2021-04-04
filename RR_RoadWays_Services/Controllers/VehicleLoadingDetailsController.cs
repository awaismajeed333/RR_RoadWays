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
    public class VehicleLoadingDetailsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var context = new RRRoadwaysDBContext();
            var rRRoadwaysDBContext = context.VehicleLoadingDetail.Include(v => v.Vehicle);
            return View(await rRRoadwaysDBContext.ToListAsync());
        }

        [HttpGet]
        public ActionResult getLoadingDetails()
        {
            var context = new RRRoadwaysDBContext();
            //var dataVehicleLoadingDetail = context.VehicleLoadingDetail.ToList().OrderByDescending(x => x.Id);

            List<VehicleLoadingDetail> vehicleLoadings = context.VehicleLoadingDetail.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();

            var dataVehicleLoadingDetail = (from vl in vehicleLoadings
                                     join v in vehicles on vl.VehicleId equals v.Id
                                     select new { vl.Id,vl.LoadingId, v.VehicleNumber, LoadingDate = vl.LoadingDate.GetValueOrDefault().ToString("dddd, dd MMMM yyyy"), vl.VehicleName, vl.Description }
                                     ).ToList();

            return Json(new { data = dataVehicleLoadingDetail }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.voucherId = new SelectList(context.Voucher.Where(x => x.IsDeleted == false).ToList(), "Id", "VoucherNumber");


            return View(new VehicleLoadingDetail());
        }

        [HttpPost]
        public IActionResult Add(VehicleLoadingDetail data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                data.EntryDate = DateTime.Now.Date;
                context.Add(data);
                context.SaveChanges();

                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.voucherId = new SelectList(context.Voucher.Where(x => x.IsDeleted == false).ToList(), "Id", "VoucherNumber");

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
            var std = context.VehicleLoadingDetail.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.voucherId = new SelectList(context.Voucher.Where(x => x.IsDeleted == false).ToList(), "Id", "VoucherNumber");

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(VehicleLoadingDetail data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dbEntry = context.Entry(data);
                dbEntry.Property("LoadingId").IsModified = true;
                dbEntry.Property("VehicleId").IsModified = true;
                dbEntry.Property("VehicleName").IsModified = true;
                dbEntry.Property("Description").IsModified = true;

                context.SaveChanges();
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.voucherId = new SelectList(context.Voucher.Where(x => x.IsDeleted == false).ToList(), "Id", "VoucherNumber");

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
                var dataVehicleLoadingDetail = context.VehicleLoadingDetail.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataVehicleLoadingDetail);
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
