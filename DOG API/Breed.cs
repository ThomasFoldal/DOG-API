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
            _breed.Replace(",", string.Empty);
            breed = _breed.Replace("\"", string.Empty);

        }
        public Breed(string _breed, string[] _subBreeds)
        {
            _breed = _breed.Replace(",", string.Empty);
            _breed = _breed.Replace(":", string.Empty);
            _breed = _breed.Replace("\"", string.Empty);
            _breed = _breed.Replace("\\", string.Empty);
            breed = _breed;
            for (int i = 0; i < _subBreeds.Count(); i++)
            {
                _subBreeds[i] = _subBreeds[i].Replace(",", string.Empty);
                _subBreeds[i] = _subBreeds[i].Replace("\"", string.Empty);
                _subBreeds[i] = _subBreeds[i].Replace("\\", string.Empty);
            }
            subBreeds = _subBreeds;
        }
    }
}