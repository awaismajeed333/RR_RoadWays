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
    public class VouchersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var context = new RRRoadwaysDBContext();
            //var rRRoadwaysDBContext = context.Voucher;
            //ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");
            //return View(await rRRoadwaysDBContext.ToListAsync());
            return View();
        }

        [HttpGet]
        public ActionResult getVoucherDetails()
        {
            var context = new RRRoadwaysDBContext();
            //var dataVehicleLoadingDetail = context.VehicleLoadingDetail.ToList().OrderByDescending(x => x.Id);

            List<Voucher> Vouchers = context.Voucher.ToList();
            List<Vehicle> vehicles = context.Vehicle.Where(x => x.IsDeleted == false).ToList();

            var dataVehicleLoadingDetail = (from vchr in Vouchers
                                            join v in vehicles on vchr.VehicleNumber equals v.Id
                                            select new
                                            {
                                                vchr.Id,
                                                vchr.VoucherNumber,
                                                v.VehicleNumber,
                                                CreatedDate = vchr.CreatedDate.GetValueOrDefault().ToString("MMMM dd, yyyy"),
                                                vchr.Month,
                                                UpDate = vchr.UpDate.GetValueOrDefault().ToString("MMMM dd, yyyy"),
                                                DownReturnDate = vchr.DownReturnDate.GetValueOrDefault().ToString("MMMM dd, yyyy"),
                                                vchr.UpAmount,
                                                vchr.DownAmount
                                            }
                         ).ToList();

            return Json(new { data = dataVehicleLoadingDetail }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x=> x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.UpFrom = new SelectList(context.Company.ToList(), "Id", "Name");
            ViewBag.UpTo = new SelectList(context.City.ToList(), "Id", "Name");

            ViewBag.DownFrom = new SelectList(context.City.ToList(), "Id", "Name");
            ViewBag.DownTo = new SelectList(context.City.ToList(), "Id", "Name");

            ViewBag.PumpId = new SelectList(context.Station.Where(x=>x.StationType.ToLower().Contains("pump")).ToList(), "Id", "Name");

            ViewBag.ExpanseHead = new SelectList(context.ExpanseHead.ToList(), "Id", "HeadName");

            ViewBag.OilShop = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("oilshop")).ToList(), "Id", "Name");

            return View(new Voucher());
        }

        [HttpPost]
        public IActionResult Add(Voucher data)
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
            return View("Add");
        }



        public ActionResult SaveVoucher([FromBody] Voucher data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                string count = (context.Voucher.ToList().Count + 1).ToString();
                string voucherNumber = "VCHR" + count.PadLeft(4, '0');
                data.VoucherNumber = voucherNumber;
                data.CreatedDate = DateTime.Now.Date;
                context.Add(data);
                context.SaveChanges();
                
                var id = data.VoucherNumber;
                //ViewBag.vehicleId = new SelectList(context.Vehicle.ToList(), "Id", "VehicleNumber");
                ViewBag.result = id+"**Record Saved Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            ModelState.Clear();
            return Json(ViewBag.result , new Newtonsoft.Json.JsonSerializerSettings());
            //return RedirectToAction("Index", ViewBag.result);

        }

        public ActionResult Details(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var dataVoucher = context.Voucher
        .Where(f => f.Id == Id)
        .Include(f => f.VoucherDieselDetails)
        .Include(f => f.VoucherOthersExpenses)
        .FirstOrDefault();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            return View(dataVoucher);
        }


        public ActionResult Edit(int Id)
        {
            var context = new RRRoadwaysDBContext();

            ViewBag.result = "";
            ViewBag.error = "";

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.UpFrom = new SelectList(context.Company.ToList(), "Id", "Name");
            ViewBag.UpTo = new SelectList(context.City.ToList(), "Id", "Name");

            ViewBag.DownFrom = new SelectList(context.City.ToList(), "Id", "Name");
            ViewBag.DownTo = new SelectList(context.City.ToList(), "Id", "Name");

            ViewBag.PumpId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("pump")).ToList(), "Id", "Name");

            ViewBag.ExpanseHead = new SelectList(context.ExpanseHead.ToList(), "Id", "HeadName");

            ViewBag.OilShop = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("oilshop")).ToList(), "Id", "Name");


            var std = context.Voucher.Where(s => s.Id == Id).FirstOrDefault();
            std.VoucherDieselDetails = context.VoucherDieselDetails.Where(s => s.VoucherId == std.Id).ToList();
            std.VoucherOthersExpenses = context.VoucherOthersExpenses.Where(s => s.VoucherId == std.Id).ToList();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Voucher data)
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
                var dataVoucher = context.Voucher
        .Where(f => f.Id == id)
        .Include(f => f.VoucherDieselDetails)
        .Include(f => f.VoucherOthersExpenses)
        .FirstOrDefault();
                context.Voucher.Remove(dataVoucher);
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