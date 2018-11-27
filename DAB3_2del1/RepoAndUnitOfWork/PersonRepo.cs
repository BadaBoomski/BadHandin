using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_2del1.Interfaces;
using DAB3_2del1.Models;
using Microsoft.EntityFrameworkCore;

namespace DAB3_2del1.RepoAndUnitOfWork
{
    public class PersonRepo : Repository<Person>, IPersonRepo, IDisposable
    {
        private PersonDBContext context;

        public PersonRepo(PersonDBContext context) : base(context)
        {
        }

        public Person GetPersonWithEmailID(int id)
        {
            return PersonDBContext.Persons
                .Where(a => a.PersonID == id)
                .Include(a => a.Emails)
                .FirstOrDefault();
        }

        public IEnumerable<Person> GetAllWithEmails()
        {
            return PersonDBContext.Persons.Include(a => a.Emails);
        }

        public IEnumerable<Person> GetAllWithAddressID(int id)
        {
            return PersonDBContext.Persons.Include(a => a.AddressID);
        }

        public PersonDBContext PersonDBContext
        {
            get { return Context as PersonDBContext; }
        }

        public void RemovePerson(int id)
        {
            Person person = context.Persons.Find(id);
            context.Persons.Remove(person);
        }

        public void UpdatePerson(Person per)
        {
            context.Entry(per).State = EntityState.Modified;
        }

        public void InsertPerson(Person per)
        {
            context.Persons.Add(per);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}