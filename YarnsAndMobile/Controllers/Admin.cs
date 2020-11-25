using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YarnsAndMobile.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportData()
        {
            return View();
        }
    }
}
