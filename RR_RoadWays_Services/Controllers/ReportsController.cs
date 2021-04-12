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
    public class ReportsController : Controller
    {
        public IActionResult OilShop()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.OilShop = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("oilshop")).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult getOilShopData(OilShopModel data)
        {
            var context = new RRRoadwaysDBContext();
            int oildshopid = Convert.ToInt32(data.OilShopId);
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            if (data.VehicleNumber == null)
            {
                var vouchers = context.Voucher.Join(context.Vehicle, vo => vo.VehicleNumber, v => v.Id, (vo, v) => new
                {
                    OilShopId = vo.OilShopId,
                    Date = vo.CreatedDate,
                    Vehicle = v.VehicleNumber,
                    Amount = vo.OilAmount
                }).Where(c => c.OilShopId == oildshopid
                && c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

                List<OilShopReport> griddata = new List<OilShopReport>();
                if (vouchers.Count > 0)
                {
                    foreach (var item in vouchers)
                    {
                        OilShopReport obj = new OilShopReport()
                        {
                            SerialNo = griddata.Count + 1,
                            Date = item.Date.Value.Date,
                            Amount = item.Amount,
                            VehicleNumber = item.Vehicle
                        };

                        griddata.Add(obj);
                    }
                }

                return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else {
                var vouchers = context.Voucher.Join(context.Vehicle, vo => vo.VehicleNumber, v => v.Id, (vo, v) => new
                {
                    OilShopId = vo.OilShopId,
                    Date = vo.CreatedDate,
                    VehicleNumber = v.Id,
                    Vehicle = v.VehicleNumber,
                    Amount = vo.OilAmount
                }).Where(c => c.OilShopId == oildshopid
                && c.VehicleNumber == vehicleNumber
                && c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

                List<OilShopReport> griddata = new List<OilShopReport>();
                if (vouchers.Count > 0)
                {
                    foreach (var item in vouchers)
                    {
                        OilShopReport obj = new OilShopReport()
                        {
                            SerialNo = griddata.Count + 1,
                            Date = item.Date.Value.Date,
                            Amount = item.Amount,
                            VehicleNumber = item.Vehicle
                        };

                        griddata.Add(obj);
                    }
                }

                return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }

        public IActionResult Pump()
        {
            var context = new RRRoadwaysDBContext();
            ViewBag.vehicleId = new SelectList(context.Vehicle.Where(x => x.IsDeleted == false).ToList(), "Id", "VehicleNumber");
            ViewBag.PumpId = new SelectList(context.Station.Where(x => x.StationType.ToLower().Contains("pump")).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult getPumpData(PumpModel data)
        {
            var context = new RRRoadwaysDBContext();
            int pumpid = Convert.ToInt32(data.PumpId);
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            if (data.VehicleNumber == null)
            {

                var pumpdata = context.Vehicle
                    .Join(context.Voucher,
                    v => v.Id,
                    vo => vo.VehicleNumber,
                    (v, vo) => new { v, vo })
                    .Join(context.VoucherDieselDetails,
                    c => c.vo.Id,
                    vdd => vdd.VoucherId,
                    (c, vdd) => new
                    {
                        PumpId= vdd.StationId,
                        Date = c.vo.CreatedDate,
                        VehicleNumber = c.v.Id,
                        Vehicle = c.v.VehicleNumber,
                        Litre = vdd.Litre,
                        Rate = vdd.Rate,
                        Amount = vdd.Amount,
                        OilAndOthers = vdd.OilAndOthers,
                    }).Where(c => c.PumpId == pumpid
                && c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();


                List<PumpReport> griddata = new List<PumpReport>();
                if (pumpdata.Count > 0)
                {
                    foreach (var item in pumpdata)
                    {
                        PumpReport obj = new PumpReport()
                        {
                            SerialNo = griddata.Count + 1,
                            Date = item.Date.Value.Date,
                            Vehicle = item.Vehicle,
                            Litre = item.Litre,
                            Rate = item.Rate,
                            Amount = (item.Litre * item.Rate),
                            OilAndOther = item.OilAndOthers,
                            OilAmount = item.Amount,
                            Total = (item.Litre * item.Rate) + item.Amount,
                        };

                        griddata.Add(obj);
                    }
                }

                return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                var pumpdata = context.Vehicle
                   .Join(context.Voucher,
                   v => v.Id,
                   vo => vo.VehicleNumber,
                   (v, vo) => new { v, vo })
                   .Join(context.VoucherDieselDetails,
                   c => c.vo.Id,
                   vdd => vdd.VoucherId,
                   (c, vdd) => new
                   {
                       PumpId = vdd.StationId,
                       Date = c.vo.CreatedDate,
                       VehicleNumber = c.v.Id,
                       Vehicle = c.v.VehicleNumber,
                       Litre = vdd.Litre,
                       Rate = vdd.Rate,
                       Amount = vdd.Amount,
                       OilAndOthers = vdd.OilAndOthers,
                   }).Where(c => c.PumpId == pumpid
               && c.VehicleNumber == vehicleNumber
               && c.Date.Value.Date >= data.StartDate.Date
               && c.Date.Value.Date <= data.EndDate.Date).ToList();


                List<PumpReport> griddata = new List<PumpReport>();
                if (pumpdata.Count > 0)
                {
                    foreach (var item in pumpdata)
                    {
                        PumpReport obj = new PumpReport()
                        {
                            SerialNo = griddata.Count + 1,
                            Date = item.Date.Value.Date,
                            Vehicle = item.Vehicle,
                            Litre = item.Litre,
                            Rate = item.Rate,
                            Amount = (item.Litre * item.Rate),
                            OilAndOther = item.OilAndOthers,
                            OilAmount = item.Amount,
                            Total = (item.Litre * item.Rate) + item.Amount,
                        };

                        griddata.Add(obj);
                    }
                }

                return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }
    }
}