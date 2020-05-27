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
            List<Menu> menuListModel = _menuService.GetAll();

            return Json(menuListModel);
        }

        //public IActionResult SortedMenuList(bool isSlim = false, bool isCheap = false, bool isVegan = false)
        //{
        //    List<Menu> menuListModel = _menuService.GetAll();
        //    List<Menu> sortedMenuListModel = new List<Menu>();

        //    foreach (Menu menu in menuListModel)
        //    {
        //        _menuService.SetMenuAttributes(menu);
        //    }




        //    return Json(menuListModel);
        //}
    }
}