using DAL.Interfaces;
using Domain;
using Exceptions;
using Service.Interfaces;
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

        public async Task CreateAsync(Category category)
        {
            try
            {
               await _repo.AddAsync(category);
            }
            catch(QueryFailedException ex)
            {
                throw ex;
            }
        }
        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<IEnumerable<Category>> GetAllAsync(int warehouseId) => await _repo.GetAllAsync(warehouseId);

        public async Task<Category> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
    }
}
