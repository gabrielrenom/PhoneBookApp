using Microsoft.EntityFrameworkCore;
using PhoneBookApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.DAL.Context
{
    public class PhoneBookDbContext:DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {

        }

        public void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
              new Contact {Id=1,Name= "Eric Elliot",PhoneNumbers=new List<PhoneNumber> {new PhoneNumber { Id=1, ContactId=1, Number="2225556575" } }},
              new Contact {Id=2,Name= "Steve Jobs",PhoneNumbers=new List<PhoneNumber> {new PhoneNumber { Id=2, ContactId=1, Number="2204546754" } }},
              new Contact {Id=3,Name= "Fred Allen",PhoneNumbers=new List<PhoneNumber> {new PhoneNumber { Id=3, ContactId=1, Number="2106579886" } }},
              new Contact {Id=4,Name= "Steve Wozniak",PhoneNumbers=new List<PhoneNumber> {new PhoneNumber { Id=4, ContactId=1, Number="3436758786" } }},
              new Contact {Id=5,Name= "Bill Gates",PhoneNumbers=new List<PhoneNumber> {new PhoneNumber { Id=5, ContactId=1, Number="3436549688" } }}

           );
            
        }
    }
}
