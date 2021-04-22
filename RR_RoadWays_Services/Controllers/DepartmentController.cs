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
    public class DepartmentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getDepartments()
        {
            var context = new RRRoadwaysDBContext();
            var dataDepartments = context.Department.ToList().OrderByDescending(x => x.Id);
            return Json(new { data = dataDepartments }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new Department());
        }

        [HttpPost]
        public IActionResult Add(Department data)
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var context = new RRRoadwaysDBContext();
            var std = context.Department.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Department data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("DepartmentName").IsModified = true;
                
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
                var dataDepartment = context.Department.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataDepartment);
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
            var std = context.Department.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
    }
}