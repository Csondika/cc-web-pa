using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HealthBar.Controllers
{
    public class CreatorController : Controller
    {
        public IActionResult MakeIt()
        {
            return View();
        }
    }
}