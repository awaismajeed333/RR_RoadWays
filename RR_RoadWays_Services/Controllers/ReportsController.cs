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
            List<Station> OilShoplist = context.Station.Where(x => x.StationType.ToLower().Contains("oilshop")).ToList();
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
            var vouchers = context.Voucher.Join(context.Vehicle, vo => vo.VehicleNumber, v => v.Id, (vo, v) => new
            {
                VoucherNo = vo.VoucherNumber,
                OilShopId = vo.OilShopId,
                Date = vo.CreatedDate,
                VehicleNumber = v.Id,
                Vehicle = v.VehicleNumber,
                Amount = vo.OilAmount
            }).Where(c => data.OilShopId != "-1" ? c.OilShopId == oildshopid : 1==1)
            .Where(c => data.VehicleNumber != "-1" ? c.VehicleNumber == vehicleNumber : 1==1)
            .Where(c => c.Date.Value.Date >= data.StartDate.Date)
            .Where(c => c.Date.Value.Date <= data.EndDate.Date).ToList();

            if (vouchers.Count > 0)
            {
                foreach (var item in vouchers)
                {
                    OilShopReport obj = new OilShopReport()
                    {
                        SerialNo = griddata.Count + 1,
                        VoucherNo = item.VoucherNo,
                        Date = item.Date.Value.Date,
                        Amount = item.Amount,
                        VehicleNumber = item.Vehicle
                    };

                    griddata.Add(obj);
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
                       VoucherNumber = c.vo.VoucherNumber,
                       PumpId = vdd.StationId,
                       Date = vdd.Date,
                       VehicleNumber = c.v.Id,
                       Vehicle = c.v.VehicleNumber,
                       Litre = vdd.Litre,
                       Rate = vdd.Rate,
                       Amount = vdd.Amount,
                       OilAndOthers = vdd.OilAndOthers,
                   }).Where(c => data.PumpId != "-1" ? c.PumpId == pumpid : 1==1)
               .Where(c => data.VehicleNumber != "-1" ? c.VehicleNumber == vehicleNumber : 1==1)
               .Where(c => c.Date.Value.Date >= data.StartDate.Date)
               .Where(c => c.Date.Value.Date <= data.EndDate.Date).ToList();


            if (pumpdata.Count > 0)
            {
                foreach (var item in pumpdata)
                {
                    PumpReport obj = new PumpReport()
                    {
                        SerialNo = griddata.Count + 1,
                        VoucherNumber = item.VoucherNumber,
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

        public IActionResult Maintenance()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            List<Department> departmentlist = context.Department.ToList();
            departmentlist.Insert(0, new Department() { Id = -1, DepartmentName = "All" });
            List<Station> OilShoplist = context.Station.Where(x => x.StationType.ToLower().Contains("maintenanceshop")).ToList();
            OilShoplist.Insert(0, new Station() { Id = -1, Name = "All" });

            ViewBag.OilShop = new SelectList(OilShoplist, "Id", "Name");
            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            ViewBag.departmentId = new SelectList(departmentlist, "Id", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult getMaintenanceData(MaintenanceModel data) {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            int departmentId = Convert.ToInt32(data.DepartmentId);
            int shopId = Convert.ToInt32(data.ShopId);
            List<MaintenanceReport> griddata = new List<MaintenanceReport>();
            var maintenances = context.Maintenance
                    .Join(context.Station, m => m.StationId, s => s.Id, (m, s) => new { m, s })
                    .Join(context.Vehicle, v => v.m.VehicleId, s => s.Id, (v, s) => new { v, s })
                    .Join(context.Department, b => b.v.m.DepartmentId, d => d.Id, (b, d) => new
                    {
                        VehicleId = b.v.m.VehicleId,
                        VehicleNumber = b.s.VehicleNumber,
                        DepartmentId = b.v.m.DepartmentId,
                        Date = b.v.m.MaintenanceDate,
                        MaintenanceShop = b.v.s.Name,
                        MaintenanceShopId = b.v.s.Id,
                        Department = d.DepartmentName,
                        Description = b.v.m.Description,
                        Maintenance = b.v.m.MaintenanceDesc,
                        Amount = b.v.m.Amount
                    }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.DepartmentId != "-1" ? c.DepartmentId == departmentId : 1 == 1)
                    .Where(c => data.ShopId != "-1" ? c.MaintenanceShopId == shopId : 1 == 1)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleId == vehicleNumber : 1==1).ToList();

            if (maintenances.Count > 0)
            {
                foreach (var item in maintenances)
                {
                    MaintenanceReport obj = new MaintenanceReport()
                    {
                        SerialNo = griddata.Count + 1,
                        VehicleNumber = item.VehicleNumber,
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
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }
        
        public IActionResult Claim()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            return View();
        }

        [HttpPost]
        public ActionResult getClaimsData(ClaimModel data)
        {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            List<ClaimReport> griddata = new List<ClaimReport>();
            var claims = context.VehicleClaim
                    .Join(context.Vehicle, vc => vc.VehicleId, v => v.Id, (vc, v) => new
                    {
                        VoucherNumber = v.Voucher,
                        Date = vc.ClaimDate,
                        Vechicle = vc.VehicleId,
                        VechicleNumber = v.VehicleNumber,
                        Claim = vc.Claim,
                        Description = vc.Description,
                        Amount = vc.Amount
                    }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.Vechicle == vehicleNumber : 1 == 1).ToList();

            if (claims.Count > 0)
            {
                foreach (var item in claims)
                {
                    ClaimReport obj = new ClaimReport()
                    {
                        SerialNo = griddata.Count + 1,
                        Date = item.Date.Value.Date,
                        VehicleNumber = item.VechicleNumber,
                        Claim = item.Claim,
                        Description = item.Description,
                        Amount = item.Amount,
                    };

                    griddata.Add(obj);
                }
            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Installments()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            return View();
        }

        [HttpPost]
        public ActionResult getInstallmentsData(InstallmentModel data)
        {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            List<InstallmentReport> griddata = new List<InstallmentReport>();
            var installments = context.Installment
                    .Join(context.Vehicle, vc => vc.VehicleId, v => v.Id, (vc, v) => new
                    {
                        Date = vc.InstallmentDate,
                        Vechicle = vc.VehicleId,
                        VechicleNumber = v.VehicleNumber,
                        Month = vc.InstallmentsMonth,
                        Details = vc.InstallmentDetail,
                        Amount = vc.Amount
                    }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.Vechicle == vehicleNumber : 1 == 1).ToList();

            if (installments.Count > 0)
            {
                foreach (var item in installments)
                {
                    InstallmentReport obj = new InstallmentReport()
                    {
                        SerialNo = griddata.Count + 1,
                        Date = item.Date.Value.Date,
                        VehicleNumber = item.VechicleNumber,
                        InstallmentMonth = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(item.Month)),
                        InstallmentDetails = item.Details,
                        Amount = item.Amount,
                    };

                    griddata.Add(obj);
                }
            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult FixedExpense()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            return View();
        }

        [HttpPost]
        public ActionResult getFixedExpenseData(FixedExpenseModel data)
        {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            List<FixedExpenseReport> griddata = new List<FixedExpenseReport>();
            var installments = context.FixedExpanse
                    .Join(context.Vehicle, vc => vc.VehicleId, v => v.Id, (vc, v) => new
                    {
                        Date = vc.EntryDate,
                        Vechicle = vc.VehicleId,
                        VechicleNumber = v.VehicleNumber,
                        Month = vc.ExpanseMonth,
                        StaffSalary = vc.StaffSalary,
                        StaffBhatta = vc.StaffBhatta,
                        Amount = vc.StaffSalary + vc.StaffBhatta + vc.DriverRoomRent + vc.Donation + vc.FormanSalary + vc.ExtraDriversSalary + vc.ExtraExpense
                    }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.Vechicle == vehicleNumber : 1 == 1).ToList();

            if (installments.Count > 0)
            {
                foreach (var item in installments)
                {
                    FixedExpenseReport obj = new FixedExpenseReport()
                    {
                        SerialNo = griddata.Count + 1,
                        Date = item.Date.Value.Date,
                        VehicleNumber = item.VechicleNumber,
                        Month = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(item.Month)),
                        StaffBhatta = item.StaffBhatta,
                        StaffSalary = item.StaffSalary,
                        Amount = item.Amount,
                    };

                    griddata.Add(obj);
                }
            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult CashAdvance()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });
            List<Station> pickuplist = context.Station.Where(x => x.StationType.ToLower().Contains("PickupPoint")).ToList();
            pickuplist.Insert(0, new Station() { Id = -1, Name = "All" });

            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            ViewBag.pickupId = new SelectList(pickuplist, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult getCashAdvanceData(CashAdvanceModel data)
        {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            int pickpupPointId = Convert.ToInt32(data.PickupPointId);
            List<CashAdvanceReport> griddata = new List<CashAdvanceReport>();
            var cashAdnacceList = context.Advance
                    .Join(context.AdvanceDetails, a=> a.Id, ad=>ad.AdvanceId, (a,ad) => new { a, ad } )
                    .Join(context.Vehicle, a => a.a.VehicleId, v => v.Id, (a, v) => new { a, v })
                    .Join(context.Station, d => d.a.ad.StationId, s => s.Id, (d, s) => new
                    {
                        Date = d.a.ad.AdvanceDate,
                        Vechicle = d.a.a.VehicleId,
                        VechicleNumber = d.v.VehicleNumber,
                        PickupPointId = d.a.ad.StationId,
                        PickupPointName = s.Name,
                        Description = d.a.ad.Description,
                        Amount = d.a.ad.Amount
                    }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.PickupPointId != "-1" ? c.PickupPointId == pickpupPointId : 1 == 1)
                    .Where(c => data.VehicleNumber != "-1" ? c.Vechicle == vehicleNumber : 1 == 1).ToList();

            if (cashAdnacceList.Count > 0)
            {
                foreach (var item in cashAdnacceList)
                {
                    CashAdvanceReport obj = new CashAdvanceReport()
                    {
                        SerialNo = griddata.Count + 1,
                        Date = item.Date.Value.Date,
                        VehicleNumber = item.VechicleNumber,
                        PickupPoint = item.PickupPointName,
                        Description = item.Description,
                        Amount = item.Amount,
                    };

                    griddata.Add(obj);
                }
            }
            return Json(new { data = griddata }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult OverAll()
        {
            var context = new RRRoadwaysDBContext();
            List<Vehicle> vehiclelist = context.Vehicle.Where(x => x.IsDeleted == false).ToList();
            vehiclelist.Insert(0, new Vehicle() { Id = -1, VehicleNumber = "All" });

            ViewBag.vehicleId = new SelectList(vehiclelist, "Id", "VehicleNumber");
            return View();
        }

        [HttpPost]
        public ActionResult getOverallReportData(OverallModel data)
        {
            var context = new RRRoadwaysDBContext();
            int vehicleNumber = Convert.ToInt32(data.VehicleNumber);
            OverallReport reportdata = new OverallReport();
            List<FixedExpanse> expanseList = new List<FixedExpanse>();
            reportdata.Claim = context.VehicleClaim.Where(c => c.ClaimDate.Value.Date >= data.StartDate.Date)
                    .Where(c => c.ClaimDate.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleId == vehicleNumber : 1 == 1).ToList().Sum(x => x.Amount);

            reportdata.Maintenance = context.Maintenance.Where(c => c.MaintenanceDate.Value.Date >= data.StartDate.Date)
                    .Where(c => c.MaintenanceDate.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleId == vehicleNumber : 1 == 1).ToList().Sum(x => x.Amount);

            expanseList = context.FixedExpanse.Where(c => c.EntryDate.Value.Date >= data.StartDate.Date)
                    .Where(c => c.EntryDate.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleId == vehicleNumber : 1 == 1).ToList();
            reportdata.FixedExpanse = 0;
            if (expanseList.Count > 0) {
                foreach (var item in expanseList)
                {
                    reportdata.FixedExpanse += (item.StaffSalary + item.StaffBhatta + item.DriverRoomRent + item.Donation + item.FormanSalary + item.ExtraDriversSalary + item.ExtraExpense);
                }
            }

            reportdata.Installment = context.Installment.Where(c => c.EntryDate.Value.Date >= data.StartDate.Date)
                    .Where(c => c.EntryDate.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleId == vehicleNumber : 1 == 1).ToList().Sum(x => x.Amount);

            reportdata.Profit = 0;
            Decimal? totalAmdan = context.Voucher.Where(c => c.CreatedDate.Value.Date >= data.StartDate.Date)
                    .Where(c => c.CreatedDate.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleNumber == vehicleNumber : 1 == 1).ToList().Sum(x => x.DownAmount + x.UpAmount);

            Decimal? totalDieselAndOil = 0;
            Decimal? totalOil = 0;
            var totalDieselAndOilList = context.Voucher.Join(context.VoucherDieselDetails, vou => vou.Id, vou_die => vou_die.VoucherId, (vou, vou_die) => new
                            {
                                Date = vou.CreatedDate,
                                VehicleNo = vou.VehicleNumber,
                                voucherId = vou.Id,
                                OilAmount = vou.OilAmount,
                                Litre = vou_die.Litre,
                                Rate = vou_die.Rate,
                                Amount = vou_die.Amount,
                            }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleNo == vehicleNumber : 1 == 1).ToList();

            if (totalDieselAndOilList.Count > 0) {
                int tempId = 0;
                foreach (var item in totalDieselAndOilList)
                {
                    totalDieselAndOil += ((item.Litre * item.Rate) + item.Amount);
                    if (tempId == 0 || tempId != item.voucherId) {
                        tempId = item.voucherId;
                        totalDieselAndOil += item.OilAmount == null ? 0 : item.OilAmount;
                    }

                }
            }

            Decimal? totalOtherExpanse = context.Voucher.Join(context.VoucherOthersExpenses, vou => vou.Id, vou_other => vou_other.VoucherId, (vou, vou_other) => new
                            {
                                Date = vou.CreatedDate,
                                VehicleNo = vou.VehicleNumber,
                                OtherAmount = vou_other.Amount,
                            }).Where(c => c.Date.Value.Date >= data.StartDate.Date)
                    .Where(c => c.Date.Value.Date <= data.EndDate.Date)
                    .Where(c => data.VehicleNumber != "-1" ? c.VehicleNo == vehicleNumber : 1 == 1).ToList().Sum(x => x.OtherAmount);

            reportdata.Profit = (totalAmdan - (totalDieselAndOil + totalOtherExpanse));

            reportdata.TotalRemaining = (reportdata.Profit - reportdata.Claim - reportdata.Maintenance - reportdata.FixedExpanse - reportdata.Installment);

            return Json(new { data = reportdata }, new Newtonsoft.Json.JsonSerializerSettings());
        }


    }
}