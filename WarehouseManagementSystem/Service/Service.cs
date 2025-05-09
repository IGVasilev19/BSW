using Entities;

namespace Service
{
    public interface Service<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task CreateAsync(T entity);
    }
}
