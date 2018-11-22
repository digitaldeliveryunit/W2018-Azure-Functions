using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Migrations;

namespace com.petronas.myevents.api.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly Lazy<DocumentClient> _lazyClient = new Lazy<DocumentClient>(InitializeDocumentClient);
        private DocumentClient _client => _lazyClient.Value;
        private Uri _collectionUri;

        public BaseRepository(string collectionName)
        {
            var databaseName = Environment.GetEnvironmentVariable(AppSettings.DbName);
            _collectionUri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
            var createDbResult = _client.CreateDatabaseIfNotExistsAsync(
                new Database { Id = databaseName }).GetAwaiter().GetResult();

            // If database is newly created, run migrations
            if (createDbResult.StatusCode == HttpStatusCode.Created)
            {
                MigrateCollection.Run(_client);
                MigrateMasterData.Run(_client);
            }
        }

        public IQueryable<T> GetAll()
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = -1,
                EnableCrossPartitionQuery = true
            };
            var result = _client.CreateDocumentQuery<T>(_collectionUri, feedOptions);
            return result;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, FeedOptions feedOptions)
        {
            var result = _client.CreateDocumentQuery<T>(_collectionUri, feedOptions).Where(predicate);
            return result;
        }

        public IQueryable<T> GetAll(string sqlExpression, FeedOptions feedOptions)
        {
            var result = _client.CreateDocumentQuery<T>(_collectionUri, sqlExpression, feedOptions);
            return result;
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
