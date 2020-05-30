using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthBar.Models;
using HealthBar.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthBar.Controllers
{
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

        public void RefreshMenuActivity()
        {
            
        }
    }
}