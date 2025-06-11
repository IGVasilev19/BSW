using DAL;
using Domain;
using Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService (IProductRepository repo)
        {
            _repo = repo;
        }

        public Task CreateAsync(Product product)
        {
            try
            {
                _repo.AddAsync(product);
            }
            catch(QueryFailedException ex) 
            {
                throw ex;
            }

            return null;
        }

        public async Task<int> CreateAsync(Product product, SqlConnection connection, SqlTransaction transaction) => await _repo.AddAsync(product, connection, transaction);

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();

        public async Task<Product> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
    }
}
