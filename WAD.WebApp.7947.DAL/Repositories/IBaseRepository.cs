using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WAD.WebApp._7947.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
    }
}
