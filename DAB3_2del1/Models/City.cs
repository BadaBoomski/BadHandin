using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_2del1.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string Cityname { get; set; }
        public string Postcode { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
