using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RR_RoadWays_Services.Controllers
{
    public class UnauthorizedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}