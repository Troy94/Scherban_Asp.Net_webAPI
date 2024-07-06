using Fenrir_WebApplication.Model;

namespace Fenrir_WebApplication.RepositoryPattern
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPersonsAsync();
        Task<Person> GetPersonByIdAsync(Guid id);
        Task<Person> CreatePersonAsync(Person person);
        Task<Person> UpdatePersonAsync(Guid id, Person person);
        Task<bool> DeletePersonAsync(Guid id);
    }
}
