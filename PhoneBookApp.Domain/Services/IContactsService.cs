using PhoneBookApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Domain.Services
{
    public interface IContactsService
    {
        Task<IEnumerable<ContactModel>> GetAllContacts();
        Task<ContactModel> GetContactById(int id);
        Task<ContactModel> CreateContact(ContactModel contact);
        Task<ContactModel> UpdateContact(ContactModel contact);
        Task DeleteContact(int id);
        Task<ContactModel> GetContactByName(string name);
    }
}
