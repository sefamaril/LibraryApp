namespace LibraryApp.DataAccess.Abstract
{
    public interface IGenericRepository<TEntity, in TKey> where TEntity : BaseEntity<TKey>
    {
        Task<bool> AddAsync(TEntity entity, Guid userId);

        Task<bool> UpdateAsync(TEntity entity, Guid userId);

        Task<bool> SoftDeleteAsyncById(TKey id, Guid userId);

        Task<int> GetCountAsync();

        Task<TEntity> GetByIdAsync(TKey id);

        Task<IEnumerable<TEntity>> GetListAsync();
    }
}