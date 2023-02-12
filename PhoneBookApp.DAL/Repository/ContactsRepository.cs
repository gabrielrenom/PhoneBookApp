using Microsoft.EntityFrameworkCore;
using PhoneBookApp.DAL.Context;
using PhoneBookApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.DAL.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private IGenericRepository<Contact> _genericRepository;
        private readonly PhoneBookDbContext _context;
        public ContactsRepository(PhoneBookDbContext context)
        {
            _context = context;
            _genericRepository = new GenericRepository<Contact>(_context);
        }
        
        public async Task<Contact> CreateContact(Contact contact)
        {
            var result = await _genericRepository.Insert(contact);

            return result;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var entity = await _genericRepository.GetById(id);
            var telephones = await _context.PhoneNumbers.ToListAsync();
            if (telephones != null)
            {
                _context.RemoveRange(telephones);
                await _context.SaveChangesAsync();
            }
            var result = await _genericRepository.Delete(entity);
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var result = await _context.Contacts.Include(x=>x.PhoneNumbers).ToListAsync();
            return result;
        }

        public async Task<Contact> GetContactById(int id)
        {
            var result = await _genericRepository.GetById(id);
            return result;
        }

        public Task<Contact> GetContactByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            var result = await _genericRepository.Update(contact);
            return contact;
        }

    }
}
