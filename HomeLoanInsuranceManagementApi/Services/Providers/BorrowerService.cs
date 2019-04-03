using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeLoanInsuranceManagementApi.DataBaseContext;
using HomeLoanInsuranceManagementApi.Models;
using HomeLoanInsuranceManagementApi.Services.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeLoanInsuranceManagementApi.Services.Providers
{
    public class BorrowerService : IBorrowerService
    {
        private readonly MongoDBContext _context = null;

        public BorrowerService(IOptions<Settings> settings)
        {
            _context = new MongoDBContext(settings);
        }

        // Get all Banks
        public async Task<IEnumerable<Borrower>> GetAll()
        {
            try
            {
                return await _context.Borrower.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        public async Task<Borrower> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Borrower.Find(borrower => borrower.Id == id || borrower.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result> Add(Borrower borrower)
        {
            Result result = new Result() { IsSuccess = true, Message = "Message" };

            try
            {
                await _context.Borrower.InsertOneAsync(borrower);

            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        public async Task<Result> Remove(string id)
        {
            Result result = new Result();
            try
            {
                DeleteResult actionResult = await _context.Borrower.DeleteOneAsync(Builders<Borrower>.Filter.Eq("Id", id));
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "Borrower Record Removed";
                
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        public async Task<bool> UpdateBorrower(string id, Borrower borrower)
        {
            

            try
            {
                UpdateResult actionResult = await _context.Borrower.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Result> Update(string id, Borrower bank)
        {
            Result result = new Result();
            try
            {
                var filter = Builders<Borrower>.Filter.Eq(s => s.Id, id);
                var update = Builders<Borrower>.Update.Set(s => s.Comments, borrower.Comments).
                             CurrentDate(s => s.UpdatedOn);


                ReplaceOneResult actionResult = await _context.Borrower.ReplaceOneAsync(n => n.Id.Equals(id),
                                                                                      bank,
                                                                                      new UpdateOptions { IsUpsert = true });

                result.IsSuccess = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                result.Message = "Borrower Record Updated";               
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        // Cleanup
        public async Task<Result> RemoveAll()
        {
            Result result = new Result();
            try
            {
                DeleteResult actionResult = await _context.Borrower.DeleteManyAsync(new BsonDocument());
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "All Borrowers Records Deleted";
                
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public Task<Property> GetPropertyDetails()
        {
            throw new NotImplementedException();
        }

        public Task<Loan> GetLoanDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<InsurancePolicy>> GetInsurancePoliciesDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<Property>> GetPropertiesDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<Loan>> GetLoansDetails()
        {
            throw new NotImplementedException();
        }
    }
}
