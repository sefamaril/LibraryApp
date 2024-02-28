using LibraryApp.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DataAccess.Concrete
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly LibraryContext _context;

        public GenericRepository(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }

        public async Task<bool> AddAsync(TEntity entity, Guid userId)
        {
            try
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedUser = userId;

                await _context.AddAsync(entity);
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Set<TEntity>().CountAsync(i => !i.DeletedDate.HasValue);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<bool> SoftDeleteAsyncById(TKey id, Guid userId)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            entity.DeletedDate = DateTime.UtcNow;
            entity.DeletedUser = userId;

            _context.Attach<TEntity>(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity, Guid userId)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedUser = userId;

            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}