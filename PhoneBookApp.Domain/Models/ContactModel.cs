using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Domain.Models
{
    public class ContactModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
    }
}
