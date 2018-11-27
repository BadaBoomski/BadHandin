using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_2del1.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Streetname { get; set; }
        public string Housenumber { get; set; }

        public int CityID { get; set; }
        public City City { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}
