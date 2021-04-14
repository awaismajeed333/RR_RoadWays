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
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            List<Station> OilShoplist = context.Station.Where(x => x.StationType.ToLower().Contains("pump")).ToList();
            OilShoplist.Insert(0, new Station() { Id = -1, Name = "All" });

            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            ViewBag.OilShop = new SelectList(OilShoplist, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult getOilShopData(OilShopModel data)
        {
            var context = new RRRoadwaysDBContext();
            int oildshopid = Convert.ToInt32(data.OilShopId);
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            List<OilShopReport> griddata = new List<OilShopReport>();
            if (data.VehicleNumber == "-1" && data.OilShopId == "-1")
            {
                var vouchers = context.Voucher.Join(context.Vehicle, vo => vo.VehicleNumber, v => v.Id, (vo, v) => new
                {
                    OilShopId = vo.OilShopId,
                    Date = vo.CreatedDate,
                    Vehicle = v.VehicleNumber,
                    Amount = vo.OilAmount
                }).Where(c => c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

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

            }
            else if (data.VehicleNumber != "-1" && data.OilShopId == "-1") {
                var vouchers = context.Voucher.Join(context.Vehicle, vo => vo.VehicleNumber, v => v.Id, (vo, v) => new
                {
                    OilShopId = vo.OilShopId,
                    Date = vo.CreatedDate,
                    VehicleNumber = v.Id,
                    Vehicle = v.VehicleNumber,
                    Amount = vo.OilAmount
                }).Where(c => c.VehicleNumber == vehicleNumber
                && c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

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
            }
            else if (data.VehicleNumber == "-1" && data.OilShopId != "-1")
            {
                var vouchers = context.Voucher.Join(context.Vehicle, vo => vo.VehicleNumber, v => v.Id, (vo, v) => new
                {
                    OilShopId = vo.OilShopId,
                    Date = vo.CreatedDate,
                    VehicleNumber = v.Id,
                    Vehicle = v.VehicleNumber,
                    Amount = vo.OilAmount
                }).Where(c => c.OilShopId == oildshopid
                && c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

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
            }
            else
            {
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
            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Pump()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> pumplist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            pumplist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            List<Station> stationlist = context.Station.Where(x => x.StationType.ToLower().Contains("pump")).ToList();
            stationlist.Insert(0, new Station() { Id = -1, Name = "All" });
            ViewBag.vehicleId = new SelectList(pumplist, "Id", "VehicleNumber");
            ViewBag.PumpId = new SelectList(stationlist, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult getPumpData(PumpModel data)
        {
            var context = new RRRoadwaysDBContext();
            int pumpid = Convert.ToInt32(data.PumpId);
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            List<PumpReport> griddata = new List<PumpReport>();
            if (data.VehicleNumber == "-1" && data.PumpId == "-1")
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
                    }).Where(c => c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();


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
            }
            else if (data.VehicleNumber == "-1" && data.PumpId != "-1")
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
                && c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

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
            }
            else if (data.VehicleNumber != "-1" && data.PumpId == "-1")
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
                   }).Where(c => c.VehicleNumber == vehicleNumber
               && c.Date.Value.Date >= data.StartDate.Date
               && c.Date.Value.Date <= data.EndDate.Date).ToList();


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

            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Maintenance()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            return View();
        }

        [HttpPost]
        public ActionResult getMaintenanceData(MaintenanceModel data) {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            List<MaintenanceReport> griddata = new List<MaintenanceReport>();
            if (data.VehicleNumber == "-1")
            {
                var maintenances = context.Maintenance
                    .Join(context.Station, m => m.StationId, s => s.Id, (m, s) => new {m,s })
                    .Join(context.Department, b => b.m.DepartmentId , d => d.Id, (b, d) => new
                    {
                        Date = b.m.MaintenanceDate,
                        MaintenanceShop = b.s.Name,
                        Department = d.DepartmentName,
                        Description = b.m.Description,
                        Maintenance = b.m.MaintenanceDesc,
                        Amount = b.m.Amount
                }).Where(c => c.Date.Value.Date >= data.StartDate.Date
                && c.Date.Value.Date <= data.EndDate.Date).ToList();

                if (maintenances.Count > 0)
                {
                    foreach (var item in maintenances)
                    {
                        MaintenanceReport obj = new MaintenanceReport()
                        {
                            SerialNo = griddata.Count + 1,
                            Date = item.Date.Value.Date,
                            MaintenanceShop = item.MaintenanceShop,
                            Department = item.Department,
                            Description = item.Description,
                            Maintenance= item.Maintenance,
                            Amount = item.Amount,
                        };

                        griddata.Add(obj);
                    }
                }

            }
            else
            {
                var maintenances = context.Maintenance
                    .Join(context.Station, m => m.StationId, s => s.Id, (m, s) => new { m, s })
                    .Join(context.Department, b => b.m.DepartmentId, d => d.Id, (b, d) => new
                    {
                        VehicleId = b.m.VehicleId,
                        Date = b.m.MaintenanceDate,
                        MaintenanceShop = b.s.Name,
                        Department = d.DepartmentName,
                        Description = b.m.Description,
                        Maintenance = b.m.MaintenanceDesc,
                        Amount = b.m.Amount
                    }).Where(c => c.VehicleId == vehicleNumber
                    && c.Date.Value.Date >= data.StartDate.Date
                    && c.Date.Value.Date <= data.EndDate.Date).ToList();

                if (maintenances.Count > 0)
                {
                    foreach (var item in maintenances)
                    {
                        MaintenanceReport obj = new MaintenanceReport()
                        {
                            SerialNo = griddata.Count + 1,
                            Date = item.Date.Value.Date,
                            MaintenanceShop = item.MaintenanceShop,
                            Department = item.Department,
                            Description = item.Description,
                            Maintenance = item.Maintenance,
                            Amount = item.Amount,
                        };

                        griddata.Add(obj);
                    }
                }
            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}