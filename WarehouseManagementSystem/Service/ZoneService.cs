using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ZoneService : IZoneService
    {
        public Task CreateAsync(Zone entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Zone>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Zone> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
