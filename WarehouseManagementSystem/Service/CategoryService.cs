using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService (ICategoryRepository repo)
        {
            _repo = repo;
        }

        public Task CreateAsync(Category category) => _repo.AddAsync(category);

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
