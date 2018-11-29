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


namespace DAB3_2del1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonRepoController : ControllerBase
    {
        //private readonly PersonDBContext _context;

        //private UnitOfWork unitOfWork;

        private IUnitOfWork unitOfWork;

        private IPersonRepo personRepo;

        public PersonRepoController(IUnitOfWork unitOfWork) 
        {
            //this.personRepo = personRepo;
            this.unitOfWork = unitOfWork;
        }

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
        public IActionResult PutPerson([FromRoute] int id, [FromBody] Person person)
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
            unitOfWork.Persons.UpdatePerson(person);
            //var person = unitOfWork.UpdatePerson(person);
            unitOfWork.Complete();

            // Using Repository.
            //personRepo.UpdatePerson(person);
            //personRepo.Save();

           return Ok(person);
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