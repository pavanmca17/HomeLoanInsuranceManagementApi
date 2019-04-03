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

        // get a collection of Banks
        public IMongoCollection<Bank> Banks
        {
            get
            {               
                return _mongoDatabase.GetCollection<Bank>("Bank");
            }

        }

        // get a collection of InsuranceCompany
        public IMongoCollection<InsuranceCompany> InsuranceCompany
        {
            get
            {
                return _mongoDatabase.GetCollection<InsuranceCompany>("InsuranceCompany");
            }

        }

        // get a collection of InsurancePolicies
        public IMongoCollection<InsurancePolicy> InsurancePolicy
        {
            get
            {
                return _mongoDatabase.GetCollection<InsurancePolicy>("InsurancePolicy");
            }

        }

        // get a collection of Loan
        public IMongoCollection<Loan> Loan
        {
            get
            {
                return _mongoDatabase.GetCollection<Loan>("Loan");
            }

        }

        // get a collection of Property
        public IMongoCollection<Property> Property
        {
            get
            {
                return _mongoDatabase.GetCollection<Property>("Property");
            }

        }

        // get a collection of Borrower
        public IMongoCollection<Borrower> Borrower
        {
            get
            {
                return _mongoDatabase.GetCollection<Borrower>("Borrower");
            }

        }

        

        




    }
}
