﻿using Microsoft.EntityFrameworkCore;
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
            try
            {
                var result = await _genericRepository.Insert(contact);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteContact(int id)
        {
            try
            {
                var entity = await _genericRepository.GetById(id);
                var telephones = await _context.PhoneNumbers.Where(x => x.ContactId == id).ToListAsync();
                if (telephones != null)
                {
                    _context.RemoveRange(telephones);
                    await _context.SaveChangesAsync();
                }
                var result = await _genericRepository.Delete(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            try
            {
                var result = await _context.Contacts.Include(x => x.PhoneNumbers).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Contact> GetContactById(int id)
        {
            try
            {
                var result = await _genericRepository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Contact> GetContactByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            try
            {
                var entity = await _context.Contacts.Include(x => x.PhoneNumbers).FirstOrDefaultAsync(x => x.Id == contact.Id);

                entity.Name = contact.Name;
                entity.Surname = contact.Surname;

                if (entity.PhoneNumbers.Count > 0)
                    entity.PhoneNumbers[0].Number = contact.PhoneNumbers[0].Number;

                var result = await _genericRepository.Update(entity);
                return contact;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetDbSet(DbSet<Contact> dbset)
        {
            _genericRepository.Entities = dbset;
        }
    }

}
