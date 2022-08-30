using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DOG_API
{
    public class Breed
    {
        public string breed;
        public string[] subBreeds;
        public Breed(string _breed)
        {
            breed = _breed;
        }
        public Breed(string _breed, string[] _subBreeds)
        {
            breed = _breed;
            subBreeds = _subBreeds;
        }
    }
}