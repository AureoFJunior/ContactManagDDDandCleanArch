using ContactManag.Domain.Models;
using ContactManag.Infra.Repositories;

namespace ContactManag.Test
{
    public class ContactTest
    {
        private readonly ContactService _contactService;
        private readonly ContactRepository _contactRepository;
        private readonly ContactRepository _personRepository;

        public ContactTest()
        {
            var context = new ContextFactory().Context;
            _contactRepository = new ContactRepository(context);
            _personRepository = new ContactRepository(context);
            _contactService = new ContactService(_contactRepository);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            int contactId = await _contactRepository.GetLastId();

            Assert.NotNull(await _contactRepository.GetByIdAsync(contactId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await _contactRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            int personId = _personRepository.GetAllAsync().Result.FirstOrDefault().Id;

            Contact contact = new Contact("Whatsapp", "51991234567", personId);
            Assert.NotNull(await _contactService.CreateAsync(contact));
        }

        [Fact]
        public async void Update()
        {
            int contactId = await _contactRepository.GetLastId();

            Contact contact = _contactRepository.GetById(contactId);
            Assert.NotNull(_contactService.Update(contact));
        }

        [Fact]
        public async void Delete()
        {
            int contactId = await _contactRepository.GetLastId();
            Assert.NotNull(_contactService?.Delete(contactId));
        }
    }
}
