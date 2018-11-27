using System.Collections.Generic;
using DAB3_2del1.Models;
using DAB3_2del1.RepoAndUnitOfWork;

namespace DAB3_2del1.Interfaces
{
    public interface IPersonRepo: IRepository<Person>

    {
        Person GetPersonWithEmailID(int id);
        IEnumerable<Person> GetAllWithEmails();
        IEnumerable<Person> GetAllWithAddressID(int id);
        void RemovePerson(int id);
        void UpdatePerson(Person per);
        void InsertPerson(Person per);
        void Dispose();
        void Save();

    }
}