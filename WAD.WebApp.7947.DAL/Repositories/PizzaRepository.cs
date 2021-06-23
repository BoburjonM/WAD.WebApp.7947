using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAD.WebApp._7947.DAL.DTO;

namespace WAD.WebApp._7947.DAL.Repositories
{
    class PizzaRepository : IBaseRepository<Pizza>
    {
        protected readonly PizzaStoreDbContext _dbContext;

        protected PizzaRepository(PizzaStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Pizza>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
