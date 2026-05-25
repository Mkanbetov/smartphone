using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smartphone.Data
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options = null);
        Task<T> GetByIdAsync(int id, QueryOptions<T> options=null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync();
        Task DeleteAsync(int id);
    }
}


