using HealthBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Services
{
    public interface IMenuService
    {
        List<Menu> GetAll();
        void SetMenuAttributes(Menu menu);
    }
}
