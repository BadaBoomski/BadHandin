using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_2del1.Interfaces;
using DAB3_2del1.Models;

namespace DAB3_2del1.RepoAndUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PersonDBContext Context;

        public UnitOfWork(PersonDBContext context)
        {
            Context = context;
            Persons = new PersonRepo(Context);
        }

        public IPersonRepo Persons { get; private set; }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}