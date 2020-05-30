using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthBar.Models;
using HealthBar.Services;

namespace HealthBar.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult Menu()
        {
            return View("MenuList");
        }

        public IActionResult MenuList()
        {
            List<Menu> menuListModel = _menuService.GetActive();

            return Json(menuListModel);
        }

        public IActionResult SortedMenuList()
        {
            string checklist = Request.Form["checklist"];
            bool isSlim = bool.Parse(checklist.Split(",")[0]);
            bool isCheap = bool.Parse(checklist.Split(",")[1]);
            bool isVegan = bool.Parse(checklist.Split(",")[2]);

            List<Menu> menuListModel = _menuService.GetSorted(isSlim, isCheap, isVegan);

            return Json(menuListModel);
        }
    }
}