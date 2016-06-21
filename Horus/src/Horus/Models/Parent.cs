using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horus.Models
{
    [Serializable]
    public class Parent
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Children { get; set; }
    }
}
