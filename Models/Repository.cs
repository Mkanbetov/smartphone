using Microsoft.EntityFrameworkCore;
using Smartphone.Data;
using Smartphone.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartphone.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        protected DbSet<T> dbSet;

        public Repository(ApplicationDbContext ctx)
        {
            context = ctx;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options = null)
        {
            IQueryable<T> query = dbSet;

            if (options != null)
            {
                if (options.Where != null)
                    query = query.Where(options.Where);

                if (options.OrderBy != null)
                    query = options.OrderBy(query);

                if (options.Includes != null)
                {
                    foreach (string include in options.Includes)
                        query = query.Include(include);
                }
            }
            if (typeof(T) == typeof(Product))
            {
                query = query.Include("Brand").Include("Category");
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, QueryOptions<T> options = null)
        {
            IQueryable<T> query = dbSet;

            if (options?.Includes != null)
            {
                foreach (var include in options.Includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            T entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}


