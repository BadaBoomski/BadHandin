using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAB3_2del1.Models
{
    public class PersonDBContext : DbContext
    {
        public PersonDBContext() { }
        public PersonDBContext(DbContextOptions<PersonDBContext> options) 
            : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Email> Emails { get; set; }

    }
}
