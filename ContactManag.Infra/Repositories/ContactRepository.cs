using Microsoft.EntityFrameworkCore;
using ContactManag.Domain.Models;
using ContactManag.Infra.Context;

namespace ContactManag.Infra.Repositories
{
    public class ContactRepository : Repository<Contact>
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
    }
}
