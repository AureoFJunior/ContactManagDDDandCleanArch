using System.Collections.Generic;
using System.Linq;
using ContactManag.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ContactManag.Domain.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using AutoMapper;
using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Interfaces.Services;
using ContactManag.Web.Config;
using AutoMapper.Configuration.Conventions;

namespace ContactManag.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ContactController : Controller
    {
        private readonly IContactService<Contact> _contactService;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public ContactController(IContactRepository contactRepository,
            IContactService<Contact> contactService,
            IMapper mapper,
            IValidator validator)
        {
            _contactService = contactService;
            _contactRepository = contactRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the contacts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            IEnumerable<Contact> contacts = await _contactRepository.GetAllAsync();
            if (contacts == null)
            {
                _validator.AsNotFound("Contacts not found.");
                return NotFound();
            }

            IEnumerable<ContactDTO> contact = _mapper.Map<IEnumerable<ContactDTO>>(contacts);
            return Ok(contact);
        }

        /// <summary>
        /// Return all the contacts by person.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{personId}")]
        public async Task<IActionResult> GetContactsByPerson(int personId)
        {
            IEnumerable<Contact> contacts = await _contactRepository.GetAllByPersonAsync(personId);
            if (contacts == null)
            {
                _validator.AsNotFound("Contacts for this Person was not found.");
                return NotFound();
            }

            IEnumerable<ContactDTO> contact = _mapper.Map<IEnumerable<ContactDTO>>(contacts);
            return Ok(contact);
        }

        /// <summary>
        /// Get a contact.
        /// </summary>
        /// <param name="id">Id of the contact</param>
        /// <returns>The contact that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            Contact contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
            {
                _validator.AsNotFound("Contact not found.");
                return NotFound();
            }

            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);
            return Ok(contactDTO);
        }

        /// <summary>
        /// Add a new contact
        /// </summary>
        /// <param name="contactDTO">Contact to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactDTO contactDTO)
        {
            Contact contact = _mapper.Map<Contact>(contactDTO);
            Contact createdContact = await _contactService.CreateAsync(contact);
            contactDTO = _mapper.Map<ContactDTO>(createdContact);
            return Ok(contactDTO);
        }

        /// <summary>
        /// Update an contact
        /// </summary>
        /// <param name="contactDTO">Contact's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] ContactDTO contactDTO)
        {
            Contact contact = _mapper.Map<Contact>(contactDTO);
            Contact updatedContact = _contactService.Update(contact);
            if (updatedContact == null)
            {
                _validator.AsNotFound("Contact not found.");
                return NotFound();
            }

            contactDTO = _mapper.Map<ContactDTO>(updatedContact);
            return Ok(contactDTO);
        }

        /// <summary>
        /// Delete an contact
        /// </summary>
        /// <param name="id">Contact's ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
            Contact contact = _contactService.Delete(id);
            if (contact == null)
            {
                _validator.AsNotFound("Contact not found.");
                return NotFound();
            }

            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);
            return Ok(contactDTO);
        }
    }
}