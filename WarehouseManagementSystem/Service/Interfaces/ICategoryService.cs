using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService : Service<Category>
    {
        public Task<IEnumerable<Category>> GetAllAsync(int warehouseId);
    }
}
