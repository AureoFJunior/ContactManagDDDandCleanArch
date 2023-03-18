using ContactManag.Domain.Models;

namespace ContactManag.Domain.Interfaces.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        #region Sync
        #endregion

        #region Async
        Task<Contact> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<IEnumerable<Contact>> GetAllByPersonAsync(int personId);
        #endregion
    }
}