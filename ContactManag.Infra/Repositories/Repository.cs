using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Models;
using ContactManag.Infra.Context;

namespace ContactManag.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public Repository(AppDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        #region Sync
        public virtual TEntity GetById(int id)
        {
            var query = _context.Set<TEntity>().Where(e => e.Id == id).AsNoTracking();

            if (query.Any())
                return query.FirstOrDefault();

            return null;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = _context.Set<TEntity>().AsNoTracking();

            if (query.Any())
                return query.ToList();

            return new List<TEntity>();
        }

        public virtual TEntity Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public virtual IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entitys)
        {
            _context.Set<TEntity>().AddRange(entitys);
            _unitOfWork.Commit();
            return entitys;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entitys)
        {
            _context.Set<TEntity>().UpdateRange(entitys);
            _unitOfWork.Commit();
            return entitys;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public virtual TEntity Delete<TInclude>(TEntity entity, params Expression<Func<TEntity, TInclude>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
            {
                _context.Set<TEntity>().Include(includeProperty);
            }

            _context.Set<TEntity>().Remove(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public virtual IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entitys)
        {
            _context.Set<TEntity>().RemoveRange(entitys);
            _unitOfWork.Commit();
            return entitys;
        }
        #endregion

        #region Async
        public virtual async Task<int> GetLastId()
        {
            var query = _context.Set<TEntity>().AsNoTracking();
            if (await query.AnyAsync())
                return query.OrderByDescending(x => x.Id).FirstOrDefault().Id;

            return 0;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var query = _context.Set<TEntity>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = _context.Set<TEntity>().AsNoTracking();

            if (await query.AnyAsync())
                return await query.ToListAsync();

            return new List<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }
        public virtual async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entitys)
        {
            await _context.Set<TEntity>().AddRangeAsync(entitys);
            await _unitOfWork.CommitAsync();
            return entitys;
        }
        #endregion
    }
}