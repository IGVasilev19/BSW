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
    public class ZoneService : IZoneService
    {
        private readonly IZoneRepository _repo;

        public ZoneService(IZoneRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(Zone zone)
        {
            try
            {
                await _repo.AddAsync(zone);
            }
            catch (QueryFailedException ex)
            {
                throw;
            }
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Zone>> GetAllAsync() => _repo.GetAllAsync();
        public async Task<IEnumerable<Zone>> GetAllAsync(int warehouseId) => await _repo.GetAllAsync(warehouseId);

        public async Task<Zone> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
    }
}
