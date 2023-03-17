using Microsoft.EntityFrameworkCore;
using ContactManag.Domain.Models;
using ContactManag.Infra.Context;

namespace ContactManag.Infra.Repositories
{
    public class PersonRepository : Repository<Person>
    {
        private readonly UnitOfWork _unitOfWork;

        public PersonRepository(AppDbContext context) : base(context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var query = _context.Set<Person>().Where(c => c.Id == id).AsNoTracking();

            if (await query.AnyAsync())
            {
                return await query.FirstOrDefaultAsync();
            }

            return null;
        }

        public async override Task<IEnumerable<Person>> GetAllAsync()
        {
            var query = _context.Set<Person>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Person>();
        }
    }
}
