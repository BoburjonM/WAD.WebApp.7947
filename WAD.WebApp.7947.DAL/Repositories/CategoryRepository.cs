using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAD.WebApp._7947.DAL.DTO;

namespace WAD.WebApp._7947.DAL.Repositories
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        protected readonly PizzaStoreDbContext _dbContext;

        protected CategoryRepository(PizzaStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
