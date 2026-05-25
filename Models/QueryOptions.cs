using System;
using System.Linq;
using System.Linq.Expressions;

namespace Smartphone.Data
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, bool>> Where { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
        public string[] Includes { get; set; }
    }
}
