using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                ViewBag.error = "0**"+e.Message;
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
        .Include(vdd => vdd.VoucherDieselDetails)
        .Include(voe => voe.VoucherOthersExpenses)
        .Include(vl => vl.VehicleLoading).ThenInclude(vld => vld.VehicleLoadingDetail)
        .FirstOrDefault();

            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.UpFrom = new SelectList(context.Company.ToList(), "Id", "Name");
            ViewBag.UpTo = new SelectList(context.City.ToList(), "Id", "Name");

            ViewBag.DownFrom = new SelectList(context.City.ToList(), "Id", "Name");
            ViewBag.DownTo = new SelectList(context.City.ToList(), "Id", "Name");

            ViewBag.PumpId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("pump")).ToList(), "Id", "Name");

            ViewBag.ExpanseHead = new SelectList(context.ExpanseHead.ToList(), "Id", "HeadName");

            ViewBag.OilShop = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("oilshop")).ToList(), "Id", "Name");
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

            var dataVoucher = context.Voucher
        .Where(f => f.Id == Id)
        .Include(vdd => vdd.VoucherDieselDetails)
        .Include(voe => voe.VoucherOthersExpenses)
        .Include(vl => vl.VehicleLoading).ThenInclude(vld => vld.VehicleLoadingDetail)
        .FirstOrDefault();

            //var std = context.Voucher.Where(s => s.Id == Id).FirstOrDefault();
            //std.VoucherDieselDetails = context.VoucherDieselDetails.Where(s => s.VoucherId == std.Id).ToList();
            //std.VoucherOthersExpenses = context.VoucherOthersExpenses.Where(s => s.VoucherId == std.Id).ToList();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            //string result = JsonConvert.SerializeObject(dataVoucher, Formatting.None,
            //    new JsonSerializerSettings()
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });
            return View(dataVoucher);
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Voucher data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dbEntry = context.Entry(data);

                List<int> previousExpanseIds = context.VoucherOthersExpenses
                           .Where(ep => ep.VoucherId == data.Id)
                           .Select(ep => ep.Id)
                           .ToList();


                List<Voucher> Vouchers = context.Voucher.Where(v => v.Id == data.Id).ToList();
                List<VehicleLoading> VehicleLoadings = context.VehicleLoading.Where(v => v.VoucherId == data.Id).ToList();
                List<VehicleLoadingDetail> VehicleLoadingDetails = context.VehicleLoadingDetail.Where(x => x.IsDeleted == false).ToList();

                List<int> previousLoadingIds = (from vl in VehicleLoadings
                                                join vld in VehicleLoadingDetails on vl.Id equals vld.VloadingId
                                                select new
                                                {
                                                    vld.Id
                                                }
                         ).Select(o=>o.Id).ToList();


                List<int> previousDieselIds = context.VoucherDieselDetails
                       .Where(vl => vl.VoucherId == data.Id)
                      .Select(vl => vl.Id)
                      .ToList();


                List<int> currentLoadingDetailIds = new List<int>();
                foreach (VehicleLoading itemx in data.VehicleLoading.ToList())
                {
                    
                    foreach (VehicleLoadingDetail item in itemx.VehicleLoadingDetail.ToList())
                    {
                        currentLoadingDetailIds.Add(item.Id);
                    }
                }


                List<int> currentExpanseIds = data.VoucherOthersExpenses.Select(o => o.Id).ToList();

                List<int> currentDieselIds = data.VoucherDieselDetails.Select(o => o.Id).ToList();

                List<int> deletedExpanseIds = previousExpanseIds.Except(currentExpanseIds).ToList();
                List<int> deletedDieselIds = previousDieselIds.Except(currentDieselIds).ToList();
                List<int> deletedLoadingDetailIds = previousLoadingIds.Except(currentLoadingDetailIds).ToList();



                foreach (var deletedExpanseId in deletedExpanseIds)
                {
                    VoucherOthersExpenses deletedExpanse = context.VoucherOthersExpenses.Where(od => od.VoucherId == data.Id && od.Id == deletedExpanseId).Single();
                    context.Entry(deletedExpanse).State = EntityState.Deleted;
                }

                foreach (var ExpanseDetail in data.VoucherOthersExpenses)
                {
                    if (ExpanseDetail.Id == 0)
                    {
                        context.Entry(ExpanseDetail).State = EntityState.Added;
                        ExpanseDetail.VoucherId = data.Id;
                    }
                    else
                    {

                        context.Entry(ExpanseDetail).State = EntityState.Modified;
                        ExpanseDetail.VoucherId = data.Id;
                    }
                }

                foreach (var deleteDieselId in deletedDieselIds)
                {
                    VoucherDieselDetails deletedDiesel = context.VoucherDieselDetails.Where(od => od.VoucherId == data.Id && od.Id == deleteDieselId).Single();
                    context.Entry(deletedDiesel).State = EntityState.Deleted;
                }

                foreach (var DieselDetail in data.VoucherDieselDetails)
                {
                    if (DieselDetail.Id == 0)
                    {
                        context.Entry(DieselDetail).State = EntityState.Added;
                        DieselDetail.VoucherId = data.Id;
                    }
                    else
                    {
                        context.Entry(DieselDetail).State = EntityState.Modified;
                        DieselDetail.VoucherId = data.Id;
                    }
                }

                foreach (var deleteLoadingDetailId in deletedLoadingDetailIds)
                {
                    VehicleLoadingDetail deletedVehicleLoadingDetail = context.VehicleLoadingDetail.Where(od=> od.Id == deleteLoadingDetailId).Single();

                    context.Entry(deletedVehicleLoadingDetail).State = EntityState.Deleted;
                }

                foreach (var Loading in data.VehicleLoading)
                {
                    if (Loading.Id == 0)
                    {
                        context.Entry(Loading).State = EntityState.Added;
                        Loading.VoucherId = data.Id;
                    }
                    else
                    {
                        context.Entry(Loading).State = EntityState.Detached;
                        Loading.VoucherId = data.Id;
                    }
                    foreach (var LoadingDetail in Loading.VehicleLoadingDetail)
                    {
                        if (LoadingDetail.Id == 0)
                        {
                            context.Entry(LoadingDetail).State = EntityState.Added;
                            LoadingDetail.VloadingId = Loading.Id;
                        }
                        else
                        {
                            context.Entry(LoadingDetail).State = EntityState.Detached;
                            LoadingDetail.VloadingId = Loading.Id;
                        }

                    }
                    //context.Entry(Loading).State = EntityState.Modified;
                    //Loading.VoucherId = data.Id;
                }
                context.Entry(data).State = EntityState.Detached;
                context.SaveChanges();
                ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
                ViewBag.result = "**Record Updated Successfully!";
            }
            catch (Exception e)
            {
                var error = e;
                ViewBag.error = e.Message;
            }
            return Json(ViewBag.result, new Newtonsoft.Json.JsonSerializerSettings());
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dataVoucher = context.Voucher
       .Where(f => f.Id == id)
       .Include(vdd => vdd.VoucherDieselDetails)
       .Include(voe => voe.VoucherOthersExpenses)
       .Include(vl => vl.VehicleLoading).ThenInclude(vld => vld.VehicleLoadingDetail)
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