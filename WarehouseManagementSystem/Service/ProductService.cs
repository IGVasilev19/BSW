using DAL;
using Domain;
using Exceptions;
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

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
