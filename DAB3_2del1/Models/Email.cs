using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_2del1.Models
{
    public class Email
    {
        public int EmailID { get; set; }
        public string Emailaddress { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}
