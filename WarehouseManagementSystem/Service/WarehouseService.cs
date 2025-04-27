using DAL;
using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repo;

        public WarehouseService (IWarehouseRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Warehouse>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Warehouse> GetByIdAsync(int Id) => _repo.GetByIdAsync(Id);
        public Task CreateAsync(Warehouse warehouse) => _repo.AddAsync(warehouse);
        public Task<int> CreateAsync(Warehouse warehouse, SqlConnection connection, SqlTransaction transaction) => _repo.AddAsync(warehouse, connection, transaction);
        public Task UpdateAsync(Warehouse warehouse) => _repo.UpdateAsync(warehouse);
        public Task DeleteByIdAsync(int id) => _repo.DeleteByIdAsync(id);
    }
}
