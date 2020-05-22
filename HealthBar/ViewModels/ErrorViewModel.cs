using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthBar.ViewModels
{
    public class ErrorViewModel
    {
        public string Message { get; set; }
        public string ActionToTake { get; set; }

        public ErrorViewModel(string message, string actionToTake)
        {
            Message = message;
            ActionToTake = actionToTake;
        }
    }
}
