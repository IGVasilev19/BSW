using DAL;
using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repo;

        public AddressService (IAddressRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Address>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Address> GetByIdAsync(int Id) => _repo.GetByIdAsync(Id);
        public Task CreateAsync(Address address) => _repo.AddAsync(address);
        public Task<int> CreateAsync(Address address, SqlConnection connection, SqlTransaction transaction) => _repo.AddAsync(address, connection, transaction);
        public Task UpdateAsync(Address address) => _repo.UpdateAsync(address);
        public Task DeleteByIdAsync(int id) => _repo.DeleteByIdAsync(id);
    }
}
