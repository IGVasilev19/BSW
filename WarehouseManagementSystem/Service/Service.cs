using Domain;

namespace Service
{
    public interface Service<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task CreateAsync(T entity);
        public Task<T> GetByIdAsync(int id);

        public Task DeleteByIdAsync(int id);
    }
}
