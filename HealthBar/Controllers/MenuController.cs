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

        public IActionResult MenuList()
        {
            List<Menu> menuListModel = _menuService.GetAll();

            foreach (Menu menu in menuListModel)
            {
                _menuService.SetMenuAttributes(menu);
            }

            return View(menuListModel);
        }
    }
}