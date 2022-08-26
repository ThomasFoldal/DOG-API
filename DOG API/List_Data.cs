using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOG_API
{
    internal class List_Data
    {
        public Breed Message { get; set; }
        public string Status { get; set; }
    }
    internal class Breed
    {
        public string[] Name { get; set; }
    }
}
