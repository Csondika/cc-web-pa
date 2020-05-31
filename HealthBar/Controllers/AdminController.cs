using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthBar.Models;
using HealthBar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthBar.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IMenuService _menuService;

        public AdminController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult MenuManager()
        {
            List<Menu> menuList = _menuService.GetAll();

            return View();
        }

        public IActionResult MenuList()
        {
            List<Menu> menuList = _menuService.GetAll();

            return Json(menuList);
        }

        [HttpPost]
        public IActionResult RefreshMenuActivity()
        {
            Dictionary<int, bool> activityDict = new Dictionary<int, bool>();

            string activityString = Request.Form["activity"];
            string[] activityArray = activityString.Split(',');

            for (int i = 0; i < activityArray.Length; i+=2)
            {
                activityDict.Add(int.Parse(activityArray[i]), bool.Parse(activityArray[i + 1]));
            }

            _menuService.RefreshActive(activityDict);

            return RedirectToAction("MenuList");
        }
    }
}