using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Interfaces
{
    public class QueryResult<T>
    {
        public QueryResult(IQueryable<T> items, int? offset = null, int? limit = null)
        {
            Items = GetItems(items, offset, limit);
            Total = items.Count();
        }

        private IEnumerable<T> GetItems(IQueryable<T> items, int? offset, int? limit)
        {
            var o = 0;
            if (offset.HasValue)
                o = offset.Value;

            if (limit.HasValue)
                return items.Skip(o).Take(limit.Value);

            return items.Skip(o);
        }

        public IEnumerable<T> Items { get; }
        public int Total { get; }
    }
}