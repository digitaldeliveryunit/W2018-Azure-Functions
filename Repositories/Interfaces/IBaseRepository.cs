using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, FeedOptions feedOptions);
        IQueryable<T> GetAll(string sqlExpression, FeedOptions feedOptions);
    }
}
