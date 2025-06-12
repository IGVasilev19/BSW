using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICategoryRepository : Repository<Category>
    {
        public Task<IEnumerable<Category>> GetAllAsync(int warehouseId);
    }
}
