using Entities;

namespace Service
{
    public interface Service<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task CreateAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteByIdAsync(int id);
    }
}
