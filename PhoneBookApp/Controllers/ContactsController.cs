using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.Domain.Models;
using PhoneBookApp.Domain.Services;
using PhoneBookApp.ViewModels;
using System.Net;

namespace PhoneBookApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private IContactsService _contactsService;
        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactViewModel contact)
        {
            var result = await _contactsService.CreateContact(new ContactModel
            {
                Name = contact.Name,
                Surname = contact.Surname,
                Telephone = contact.PhoneNumber
            });
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var result = await _contactsService.GetAllContacts();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            return Ok("Hello World");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactModel contact)
        {
            return Ok("Hello World");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactsService.DeleteContact(id);
            return Ok();
        }
    }
}
