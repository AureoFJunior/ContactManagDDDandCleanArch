using Microsoft.EntityFrameworkCore;
using ContactManag.Domain.Models;
using ContactManag.Infra.Context;
using ContactManag.Domain.Interfaces.Repositories;

namespace ContactManag.Infra.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public ContactRepository(AppDbContext context) : base(context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var query = _context.Set<Contact>().Where(c => c.Id == id).AsNoTracking();

            if (await query.AnyAsync())
            {
                return await query.FirstOrDefaultAsync();
            }

            return null;
        }

        public async override Task<IEnumerable<Contact>> GetAllAsync()
        {
            var query = _context.Set<Contact>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Contact>();
        }

        public async Task<IEnumerable<Contact>> GetAllByPersonAsync(int personId)
        {
            var query = _context.Set<Contact>();
            var result = query.AsNoTracking().Where(x => x.PersonId == personId);

            return await query.AnyAsync() ? await result.ToListAsync() : new List<Contact>();
        }
    }
}
