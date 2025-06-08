using DAL;
using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repo;

        public InventoryService(IInventoryRepository repo)
        {
            _repo = repo;
        }

        public Task CreateAsync(Inventory inventory) => _repo.AddAsync(inventory);

        public async Task AddNewProductTransactionAsync(Product product, Inventory inventory)
        {
            try
            {
                await _repo.AddNewProductTransactionAsync(product, inventory);
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inventory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Inventory> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
