using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horus.Models
{
    [Serializable]
    public class Alert
    {
        public string id { get; set; }
        public Point Location { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public bool IsChild { get; set; }
        public string UserId { get; set; }
    }
}
