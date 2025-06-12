using DAL.Interfaces;
using Domain;
using Exceptions;
using Microsoft.Data.SqlClient;
using Service.Interfaces;
using Service.Strategies.Pricing;
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
        private readonly IPricingStrategy _strategy;

        public ProductService (IProductRepository repo, IPricingStrategy strategy)
        {
            _repo = repo;
            _strategy = strategy;
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

        public decimal GetFinalProductPrice(Product product)
        {
            return _strategy.CalculatePrice(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();

        public async Task<Product> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
    }
}
