using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, FeedOptions feedOptions);

        IEnumerable<T> GetBatch(Expression<Func<T, bool>> predicate, FeedOptions feedOptions,
            out string continuationKey, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        IEnumerable<T> GetBatch(SqlQuerySpec sqlExpression, FeedOptions feedOptions, out string continuationKey);

        Task<Document> Update(T item);
    }
}