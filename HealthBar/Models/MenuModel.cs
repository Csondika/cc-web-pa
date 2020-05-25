using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.Models
{
    public class MenuModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public int PriceOff { get; private set; }
    }
}
