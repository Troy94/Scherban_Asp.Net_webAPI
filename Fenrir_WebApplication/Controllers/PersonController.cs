using Fenrir_WebApplication.Model;
using Fenrir_WebApplication.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;

namespace Fenrir_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            var persons = await _repository.GetPersonsAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            var person = await _repository.GetPersonByIdAsync(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPerson = await _repository.CreatePersonAsync(person);
            return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPerson = await _repository.UpdatePersonAsync(id, person);
            if (updatedPerson == null)
                return NotFound();

            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var result = await _repository.DeletePersonAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
