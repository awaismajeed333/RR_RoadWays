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
    public class CompanyController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getCompanies()
        {
            var context = new RRRoadwaysDBContext();
            var dataCompanies = context.Company.Where(c => c.IsDeleted == false).ToList().OrderByDescending(x => x.Id);
            return Json(new { data = dataCompanies }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new Company());
        }

        [HttpPost]
        public IActionResult Add(Company data)
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.Company.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Company data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("Name").IsModified = true;
                dbEntry.Property("ContactPerson").IsModified = true;
                dbEntry.Property("ContactNumber").IsModified = true;
                

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var context = new RRRoadwaysDBContext();
                var dataCompany = context.Company.Where(c => c.Id == id).FirstOrDefault();
                dataCompany.IsDeleted = true;
                context.Update(dataCompany);
                context.SaveChanges();
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
            var std = context.Company.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
    }
}