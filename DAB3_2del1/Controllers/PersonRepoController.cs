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

        private IUnitOfWork unitOfWork;

        private IPersonRepo personRepo;

        public PersonRepoController(IUnitOfWork unitOfWork) 
        {
            // Using Repository
            //this.personRepo = personRepo;


            // Using Unit of Work
            this.unitOfWork = unitOfWork;
        }

        // GET: api/PersonRepo
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return unitOfWork.Persons.GetAll();
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

            // Using Unit of Work
            unitOfWork.Persons.InsertPerson(person);
            unitOfWork.Complete();

            // Using Repository
            //personRepo.InsertPerson(person);
            //personRepo.Save();

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

            //var person = personRepo.GetByID(id);
            var person = unitOfWork.Persons.GetByID(id);

            if (person == null)
            {
                return NotFound();
            }

            // Using Unit of Work
            unitOfWork.Persons.RemovePerson(id);
            unitOfWork.Complete();
            
            // Using Repository
            //personRepo.RemovePerson(id);
            //personRepo.Save();

            return Ok(person);
        }
    }
}