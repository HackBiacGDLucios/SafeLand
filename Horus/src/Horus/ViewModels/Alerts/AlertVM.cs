using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horus.Models;

namespace Horus.ViewModels.Alerts
{
    public class AlertVM
    {
        public Point Location { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public bool IsChild { get; set; }
        public string UserId { get; set; }
    }
}
