using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Interfaces.Services;

namespace ContactManag.Domain.Models
{
    public class ContactService : IContactService<Contact>
    {
        private readonly IRepository<Contact> _contactRepository;

        public ContactService(IRepository<Contact> ContactRepository)
        {
            _contactRepository =ContactRepository;
        }

        public async Task<Contact> CreateAsync(Contact Contact)
        {
            Contact existentContact = _contactRepository.GetById(Contact.Id);
            Contact includedContact = new Contact();

            if (existentContact == null)
            {
                includedContact = await _contactRepository.CreateAsync(Contact);
                return includedContact;
            }

            throw new ConflictDatabaseException();
        }

        public Contact Update(Contact Contact)
        {
            Contact existentContact = _contactRepository.GetById(Contact.Id);
            Contact updatedContact = new Contact();

            if (Contact != null)
            { 
                updatedContact = _contactRepository.Update(Contact);
                return updatedContact;
            }
            else
                return null;
        }

        public Contact Delete(int id)
        {
            Contact Contact = _contactRepository.GetById(id);
            Contact deletedContact = new Contact();

            if (Contact != null)
            {
                deletedContact = _contactRepository.Delete(Contact);
                return deletedContact;
            }
            else
                return null;
        }
    }
}