using ContactManag.Domain.Models;
using ContactManag.Infra.Repositories;

namespace ContactManag.Test
{
    public class PersonTest
    {
        private readonly PersonService _personService;
        private readonly PersonRepository _personRepository;

        public PersonTest()
        {
            var context = new ContextFactory().Context;
            _personRepository = new PersonRepository(context);
            _personService = new PersonService(_personRepository);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            int personId = await _personRepository.GetLastId();

            Assert.NotNull(await _personRepository.GetByIdAsync(personId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await _personRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            Person person = new Person("Teste", new DateTime(1998, 01, 01));
            Assert.NotNull(await _personService.CreateAsync(person));
        }

        [Fact]
        public async void Update()
        {
            int personId = await _personRepository.GetLastId();

            Person person = _personRepository.GetById(personId);
            Assert.NotNull(_personService.Update(person));
        }

        [Fact]
        public async void Delete()
        {
            int personId = await _personRepository.GetLastId();
            Assert.NotNull(_personService?.Delete(personId));
        }
    }
}
