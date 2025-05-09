using Domain;

namespace DAL
{
    public interface Repository<T>
    {
       public Task<IEnumerable<T>> GetAllAsync();

       public Task<T> GetByIdAsync(int id);

       public Task AddAsync(T obj);

       public Task UpdateAsync(T obj);

       public Task DeleteByIdAsync(int id);
    }
}