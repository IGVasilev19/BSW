using BLL;

namespace DAL
{
    public interface Repository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T obj);
        void Update(T obj);
        void DeleteById(int id);
    }
}