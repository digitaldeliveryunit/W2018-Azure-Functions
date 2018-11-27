using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace com.petronas.myevents.api.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : ModelBase, new()
    {
        private readonly string _collectionName;
        private readonly Uri _collectionUri;
        private readonly string _databaseName;
        private readonly Lazy<DocumentClient> _lazyClient = new Lazy<DocumentClient>(InitializeDocumentClient);

        protected BaseRepository(string collectionName)
        {
            _databaseName = Environment.GetEnvironmentVariable(AppSettings.DbName);
            _collectionName = collectionName;
            _collectionUri = UriFactory.CreateDocumentCollectionUri(_databaseName, collectionName);
        }

        private DocumentClient _client => _lazyClient.Value;

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, FeedOptions feedOptions)
        {
            if (feedOptions == null)
                feedOptions = new FeedOptions
                {
                    MaxItemCount = -1,
                    EnableCrossPartitionQuery = true
                };
            var result = _client.CreateDocumentQuery<T>(_collectionUri, feedOptions).Where(predicate);
            return result;
        }

        public IEnumerable<T> GetBatch(Expression<Func<T, bool>> predicate, FeedOptions feedOptions,
            out string continuationKey, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            if (feedOptions == null)
                feedOptions = new FeedOptions
                {
                    MaxItemCount = -1,
                    EnableCrossPartitionQuery = true,
                    EnableScanInQuery = true
                };
            else
                feedOptions.EnableScanInQuery = true;
            var result = _client.CreateDocumentQuery<T>(_collectionUri, feedOptions).Where(predicate);
            if (orderBy != null) result = orderBy(result);
            var r = result.AsDocumentQuery().ExecuteNextAsync<T>().GetAwaiter().GetResult();
            continuationKey = r.ResponseContinuation;
            return r.AsEnumerable();
        }

        public async Task<Document> Update(T item)
        {
            var documentUri = UriFactory.CreateDocumentUri(_databaseName, _collectionName, item.Id);
            var result = await _client.ReplaceDocumentAsync(documentUri, item);
            return result.Resource;
        }

        public IEnumerable<T> GetBatch(SqlQuerySpec sqlExpression, FeedOptions feedOptions, out string continuationKey)
        {
            if (feedOptions == null)
                feedOptions = new FeedOptions
                {
                    MaxItemCount = -1,
                    EnableCrossPartitionQuery = true,
                    EnableScanInQuery = true
                };
            else
                feedOptions.EnableScanInQuery = true;
            var result = _client.CreateDocumentQuery<T>(_collectionUri, sqlExpression, feedOptions);
            var r = result.AsDocumentQuery().ExecuteNextAsync<T>().GetAwaiter().GetResult();
            continuationKey = r.ResponseContinuation;
            return r.AsEnumerable();
        }

        private static DocumentClient InitializeDocumentClient()
        {
            var endpoint = new Uri(Environment.GetEnvironmentVariable(AppSettings.DbEndpoint));
            var authKey = Environment.GetEnvironmentVariable(AppSettings.DbAuthKey);
            var client = new DocumentClient(endpoint, authKey);
            client.OpenAsync().GetAwaiter().GetResult();
            return client;
        }
    }
}