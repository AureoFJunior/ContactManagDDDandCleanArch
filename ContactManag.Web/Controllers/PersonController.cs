using System.Collections.Generic;
using ContactManag.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ContactManag.Domain.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Interfaces.Services;
using ContactManag.Web.Config;

namespace ContactManag.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class PersonController : Controller
    {
        private readonly IPersonService<Person> _personService;
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public PersonController(IRepository<Person> personRepository,
            IPersonService<Person> personService,
            IMapper mapper,
            IValidator validator)
        {
            _personService = personService;
            _personRepository = personRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the persons.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            IEnumerable<Person> persons = await _personRepository.GetAllAsync();
            if (persons == null)
            {
                _validator.AsNotFound("Persons not found.");
                return NotFound();
            }

            IEnumerable<PersonDTO> person = _mapper.Map<IEnumerable<PersonDTO>>(persons);
            return Ok(person);
        }

        /// <summary>
        /// Get a person.
        /// </summary>
        /// <param name="id">Id of the person</param>
        /// <returns>The person that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            Person person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                _validator.AsNotFound("Person not found.");
                return NotFound();
            }

            PersonDTO personDTO = _mapper.Map<PersonDTO>(person);
            return Ok(personDTO);
        }

        /// <summary>
        /// Add a new person
        /// </summary>
        /// <param name="personDTO">Person to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonDTO personDTO)
        {
            Person person = _mapper.Map<Person>(personDTO);
            Person createdPerson = await _personService.CreateAsync(person);
            personDTO = _mapper.Map<PersonDTO>(createdPerson);
            return Ok(personDTO);
        }

        /// <summary>
        /// Update an person
        /// </summary>
        /// <param name="personDTO">Person's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonDTO personDTO)
        {
            Person person = _mapper.Map<Person>(personDTO);
            Person updatedPerson = _personService.Update(person);
            if (updatedPerson == null)
            {
                _validator.AsNotFound("Person not found.");
                return NotFound();
            }

            personDTO = _mapper.Map<PersonDTO>(updatedPerson);
            return Ok(personDTO);
        }

        /// <summary>
        /// Delete an person
        /// </summary>
        /// <param name="id">Person's ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int id)
        {
            Person person = _personService.Delete(id);
            if (person == null)
            {
                _validator.AsNotFound("Person not found.");
                return NotFound();
            }

            PersonDTO personDTO = _mapper.Map<PersonDTO>(person);
            return Ok(personDTO);
        }
    }
}