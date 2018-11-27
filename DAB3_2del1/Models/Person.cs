using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_2del1.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Fornavn { get; set; }
        public string Mellemnavn { get; set; }

        public string Efternavn { get; set; }

        public int AddressID { get; set; }
        public Address AddressCon { get; set; }
        public ICollection<Email> Emails { get; set; }
    }
}
