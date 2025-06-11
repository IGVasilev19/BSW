using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IZoneService : Service<Zone>
    {
        public Task<IEnumerable<Zone>> GetAllAsync(int warehouseId);
    }
}
