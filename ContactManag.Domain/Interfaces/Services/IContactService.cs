using ContactManag.Domain.Models;

namespace ContactManag.Domain.Interfaces.Services
{
    public interface IContactService<TEntity> where TEntity : class
    {
        #region Sync
        TEntity Delete(int id);
        TEntity Update(Contact contact);
        #endregion

        #region Async
        Task<TEntity> CreateAsync(Contact contact);
        #endregion
    }
}
