using BLL;

namespace Service
{
    public interface Service<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        void Create(T obj);
        void Update(T obj);
        void DeleteById(int id);
    }
}
