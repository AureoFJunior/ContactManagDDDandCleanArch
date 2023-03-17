using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Interfaces.Services;

namespace ContactManag.Domain.Models
{
    public class PersonService : IPersonService<Person>
    {
        private readonly IRepository<Person> _personRepository;

        public PersonService(IRepository<Person> PersonRepository)
        {
            _personRepository =PersonRepository;
        }

        public async Task<Person> CreateAsync(Person Person)
        {
            Person existentPerson = _personRepository.GetById(Person.Id);
            Person includedPerson = new Person();

            if (existentPerson == null)
            {
                includedPerson = await _personRepository.CreateAsync(Person);
                return includedPerson;
            }

            throw new ConflictDatabaseException();
        }

        public Person Update(Person Person)
        {
            Person existentPerson = _personRepository.GetById(Person.Id);
            Person updatedPerson = new Person();

            if (Person != null)
            { 
                updatedPerson = _personRepository.Update(Person);
                return updatedPerson;
            }
            else
                return null;
        }

        public Person Delete(int id)
        {
            Person Person = _personRepository.GetById(id);
            Person deletedPerson = new Person();

            if (Person != null)
            {
                deletedPerson = _personRepository.Delete(Person);
                return deletedPerson;
            }
            else
                return null;
        }
    }
}