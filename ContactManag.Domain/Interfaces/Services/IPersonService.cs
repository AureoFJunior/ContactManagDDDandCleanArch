using ContactManag.Domain.Models;

namespace ContactManag.Domain.Interfaces.Services
{
    public interface IPersonService<TEntity> where TEntity : class
    {
        #region Sync
        TEntity Delete(int id);
        TEntity Update(Person person);
        #endregion

        #region Async
        Task<TEntity> CreateAsync(Person person);
        #endregion
    }
}
