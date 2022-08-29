using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DOG_API
{
    internal class List_Data
    {
        public JsonObjectAttribute[] Message { get; set; }
        public string Status { get; set; }
    }
    internal class Breed
    {
        public object Name { get; set; }
    }
}
