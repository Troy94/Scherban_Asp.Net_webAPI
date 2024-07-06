using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    static HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        // Вызов GET запроса для получения списка персон
        await GetPersonsAsync();

        // Вызов POST запроса для создания новой персоны
        await CreatePersonAsync();
    }

    static async Task GetPersonsAsync()
    {      
        HttpResponseMessage response = await client.GetAsync("https://localhost:45100/api/Person");

        if (response.IsSuccessStatusCode)
        {
            List<Person> persons = await response.Content.ReadFromJsonAsync<List<Person>>();

            if (persons != null && persons.Count > 0)
            {
                Console.WriteLine("List of Persons:");
                foreach (var person in persons)
                {
                    Console.WriteLine($"{person.Id} --- {person.FirstName} {person.LastName}");
                }
            }
            else
            {
                Console.WriteLine("No persons found.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to get persons. Status code: {response.StatusCode}");
        }
    }

    static async Task CreatePersonAsync()
    {
        var newPerson = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Console",
            LastName = "TestPerson"
        };

        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:45100/api/Person", newPerson);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Person created successfully.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Failed to create person. Status code: {response.StatusCode}");
                Console.ReadLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.ReadLine();
            throw;
        }        
    }
}

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
