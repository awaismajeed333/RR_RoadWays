using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RR_RoadWays_Services.Models;

namespace RR_RoadWays_Services.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ExpanseHeadsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getExpanseHeads()
        {
            var context = new RRRoadwaysDBContext();
            var dataExpanseHead = context.ExpanseHead.ToList().OrderByDescending(x => x.Id);
            return Json(new { data = dataExpanseHead }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Add()
        {
            ViewBag.result = "";
            ViewBag.error = "";
            return View(new ExpanseHead());
        }

        [HttpPost]
        public IActionResult Add(ExpanseHead data)
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
            var std = context.ExpanseHead.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(ExpanseHead data)
        {
            try
            {
                var context = new RRRoadwaysDBContext();

                var dbEntry = context.Entry(data);
                dbEntry.Property("HeadName").IsModified = true;

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
                var dataExpanseHead = context.ExpanseHead.Where(c => c.Id == id).FirstOrDefault();
                context.Remove(dataExpanseHead);
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
            var std = context.ExpanseHead.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }
    }
}