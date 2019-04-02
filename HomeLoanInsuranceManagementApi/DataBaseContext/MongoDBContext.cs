using HomeLoanInsuranceManagementApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace HomeLoanInsuranceManagementApi.DataBaseContext
{
    public class MongoDBContext
    {
        // get a reference MongoDatabase
        private readonly IMongoDatabase _mongoDatabase = null;

        public MongoDBContext(IOptions<Settings> settings)
        {
            // Get Mongo Client
            var mongoClient = new MongoClient(settings.Value.MongoDBConnectionString);

            if (mongoClient != null)
                _mongoDatabase = mongoClient.GetDatabase(settings.Value.MongoDatabase);
        }

        // get a collection of Notes
        public IMongoCollection<Bank> Banks
        {
            get
            {               
                return _mongoDatabase.GetCollection<Bank>("Bank");
            }

        }


    }
}
