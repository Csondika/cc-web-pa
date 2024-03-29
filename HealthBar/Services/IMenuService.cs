﻿using HealthBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Services
{
    public interface IMenuService
    {
        List<Menu> GetAll();
        List<Menu> GetActive();
        List<Menu> GetSorted(bool isSlim, bool isCheap, bool isVegan);
        void RefreshActive(Dictionary<int, bool> activityDict);
    }
}
