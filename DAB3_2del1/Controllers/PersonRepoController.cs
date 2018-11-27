using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_2del1.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAB3_2del1.Models;
using DAB3_2del1.RepoAndUnitOfWork;
using Remotion.Linq.Clauses;


/// <summary>
/// WHAT HAVE WE DONE? Changed ctor and methods so they now follow the guidelines from Microsoft
/// source: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
/// This has forced us to change PersonRepo.cs + IPersonRepo.cs in order to make it work.
/// We have also outcommented the previous code, just for safety and for the group to see, what have been changed.
/// </summary>


namespace DAB3_2del1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonRepoController : ControllerBase
    {
        //private readonly PersonDBContext _context;

        private IUnitOfWork _data;

        private IPersonRepo personRepo;

        public PersonRepoController(IPersonRepo personRepo)
        {
            this.personRepo = personRepo;
        }

        //public PersonRepoController(PersonDBContext context)
        //{
        //    //_context = context;
        //}

        // GET: api/PersonRepo
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {

            //var persons = from s in personRepo.GetAll()
            //    select s;

            return personRepo.GetAll();
        }

        // GET: api/PersonRepo/5
        [HttpGet("{id}")]
        public IActionResult GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = personRepo.GetByID(id);
            //var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/PersonRepo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonID)
            {
                return BadRequest();
            }


            // Using Unit of Work.
            _data.Persons.UpdatePerson(person);
            _data.Persons.Save();

            // Using Repository.
            //personRepo.UpdatePerson(person);
            //personRepo.Save();

            //_context.Entry(person).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PersonExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/PersonRepo
        [HttpPost]
        public IActionResult PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            personRepo.InsertPerson(person);
            personRepo.Save();

            //_context.Persons.Add(person);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonID }, person);
        }

        // DELETE: api/PersonRepo/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = personRepo.GetByID(id);

            //var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            personRepo.RemovePerson(id);
            personRepo.Save();

            //_context.Persons.Remove(person);
            //await _context.SaveChangesAsync();

            return Ok(person);
        }

        //private bool PersonExists(int id)
        //{
        //    return _context.Persons.Any(e => e.PersonID == id);
        //}
    }
}