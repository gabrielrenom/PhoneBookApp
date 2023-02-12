using PhoneBookApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.DAL.Repository
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int id);
        Task<Contact> CreateContact(Contact contact);
        Task<Contact> UpdateContact(Contact contact);
        Task<bool> DeleteContact(int id);
        Task<Contact> GetContactByName(string name);
    }
}
