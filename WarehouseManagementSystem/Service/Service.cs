using Entities;

namespace Service
{
    public interface Service<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
    }
}
