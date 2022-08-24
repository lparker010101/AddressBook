using AddressBook_Parker;
using ContactModels.ContactModels;
using Fluent.Infrastructure.FluentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.ContactServices
{
    public class ContactServices
    {
        private readonly Guid _id;
        
        public ContactServices(Guid id)
        {
            _id = id;
        }

        // Add a contact (C)
        public async Task<bool> Create(Create contact)
        {
            var entity = new Person()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Address = contact.Address,
                ContactId = contact.ContactId,
            };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.Person.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        // Remove contact by ID (D)
        public async Task<bool> Delete(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Person
                    .Single(e => e.Id == id);
                ctx.Person.Remove(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        // Get contact(s) by Name (R)
        public async Task<ContactName> GetPersonByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var person =
                    await
                    ctx
                    .Person
                    .Where(d => d.Name == name)
                    .SingleOrDefaultAsync(d => d.Name == name);
                if (person is null)
                {
                    return null;
                }

                return new Person
                {
                    ContactId = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Address = person.Address,
                };
            }
        }

        // List all contacts (R)
        public async Task<IEnumerable<List>> Get()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await
                    ctx
                    .Person
                    .Select(d => new List
                    {
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Email = d.Email,
                        Address = d.Address
                    }).ToListAsync();

                return query;
            }
        }

        // Edit contact by ID (U)
        public async Task<bool> Update(Edit NewPerson, int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var person =
                    await
                    ctx
                    .Person
                    .SingleOrDefaultAsync(d => d.Id == id);
                person.Email = NewPerson.Email;
                person.ContactId = NewPerson.ContactId;
                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}