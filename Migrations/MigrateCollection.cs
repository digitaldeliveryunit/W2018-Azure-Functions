using System;
using com.petronas.myevents.api.Constants;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Migrations
{
    public static class MigrateCollection
    {
        public static void Run(DocumentClient client)
        {
            var dbName = Environment.GetEnvironmentVariable(AppSettings.DbName);
            var collections = typeof(CollectionNames).GetFields();

            foreach (var c in collections)
            {
                var collection = new DocumentCollection();
                collection.Id = c.GetValue(null).ToString();
                client.CreateDocumentCollectionAsync(
                    UriFactory.CreateDatabaseUri(dbName),
                    collection,
                    new RequestOptions { OfferThroughput = 400 }
                ).GetAwaiter().GetResult();
            }
        }
    }
}
