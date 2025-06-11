using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IZoneRepository : Repository<Zone>
    {
        public Task<IEnumerable<Zone>> GetAllAsync(int warehouseId);
    }
}
