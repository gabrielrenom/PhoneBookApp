using Microsoft.VisualBasic;
using PhoneBookApp.DAL.Entities;
using PhoneBookApp.DAL.Repository;
using PhoneBookApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Domain.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        public ContactsService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        
        public async Task<ContactModel> CreateContact(ContactModel contact)
        {
            var result = await _contactsRepository.CreateContact(ConvertFromModelToEntity(contact));
            return ConvertFromEntityToModel(result);
        }

        public async Task DeleteContact(int id)
        {
            var result = await _contactsRepository.DeleteContact(id);
        }

        public async Task<IEnumerable<ContactModel>> GetAllContacts()
        {
            var result = await _contactsRepository.GetAllContacts();
            return result.Select(x => ConvertFromEntityToModel(x));
        }

        public async Task<ContactModel> GetContactById(int id)
        {
            var result = await _contactsRepository.GetContactById(id);
            return ConvertFromEntityToModel(result);
        }

        public async Task<ContactModel> GetContactByName(string name)
        {
            var result = await _contactsRepository.GetContactByName(name);
            return ConvertFromEntityToModel(result);
        }

        public async Task<ContactModel> UpdateContact(ContactModel contact)
        {
            var result = await _contactsRepository.UpdateContact(ConvertFromModelToEntity(contact));
            return ConvertFromEntityToModel(result);
        }
        
        private ContactModel ConvertFromEntityToModel(Contact contact)
        {
            return new ContactModel { 
                CreatedAt = contact.CreatedAt, 
                CreatedBy= contact.CreatedBy, 
                Id=contact.Id,
                Name= contact.Name,
                Surname= contact.Surname,
                UpdatedAt= contact.UpdatedAt,
                UpdatedBy= contact.UpdatedBy,
                Telephone= contact.PhoneNumbers.FirstOrDefault()?.Number
            };
        }

        private Contact ConvertFromModelToEntity(ContactModel contact)
        {
            return new Contact
            {
                CreatedAt = DateTime.Now,
                CreatedBy = "Sysem",
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System",
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber{ Number=contact.Telephone}
                }
            };
        }
    }
}
