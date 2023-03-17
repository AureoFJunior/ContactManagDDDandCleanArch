using ContactManag.Domain.Models;

namespace ContactManag.Domain.Interfaces.Services
{
    public interface IUserService<TEntity> where TEntity : class
    {
        #region Sync
        public TEntity Delete(int id);
        public TEntity Update(User user);
        #endregion

        #region Async
        public Task<TEntity> CreateAsync(User user);
        #endregion
    }
}