using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fenrir_WebApplication.DBcontext;
using Fenrir_WebApplication.Model;
using Fenrir_WebApplication.RepositoryPattern;
using Microsoft.EntityFrameworkCore;

namespace Fenrir_WebApplication
{
    namespace YourNamespace.Data
    {
        public class PersonRepository : IPersonRepository
        {
            private readonly PersonDbContext _context;

            public PersonRepository(PersonDbContext context)
            {
                _context = context;
            }

            public async Task<List<Person>> GetPersonsAsync()
            {
                return await _context.Persons.ToListAsync();
            }

            public async Task<Person> GetPersonByIdAsync(Guid id)
            {
                return await _context.Persons.FindAsync(id);
            }

            public async Task<Person> CreatePersonAsync(Person person)
            {
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
                return person;
            }

            public async Task<Person> UpdatePersonAsync(Guid id, Person updatedPerson)
            {
                var person = await _context.Persons.FindAsync(id);
                if (person == null)
                    return null;

                person.FirstName = updatedPerson.FirstName;
                person.LastName = updatedPerson.LastName;

                await _context.SaveChangesAsync();
                return person;
            }

            public async Task<bool> DeletePersonAsync(Guid id)
            {
                var person = await _context.Persons.FindAsync(id);
                if (person == null)
                    return false;

                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

}
