using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;

namespace com.petronas.myevents.api.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, FeedOptions feedOptions);

        IEnumerable<T> GetBatch(Expression<Func<T, bool>> predicate, FeedOptions feedOptions, out string continuationKey, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
       
        IEnumerable<T> GetBatch(SqlQuerySpec sqlExpression, FeedOptions feedOptions, out string continuationKey);

        IQueryable<T> GetAll(string sqlExpression, FeedOptions feedOptions);
        Task<Document> Add(T item);
        Task<Document> Update(T item);
    }
}
