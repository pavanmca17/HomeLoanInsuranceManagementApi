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
    public class InsurancePolicyService : IInsurancePolicyService
    {
        private readonly MongoDBContext _context = null;

        public InsurancePolicyService(IOptions<Settings> settings)
        {
            _context = new MongoDBContext(settings);
        }

        // Get all Banks
        public async Task<IEnumerable<InsurancePolicy>> GetAll()
        {
            try
            {
                return await _context.InsurancePolicy.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        public async Task<InsurancePolicy> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.InsurancePolicy.Find(insurancePolicy => insurancePolicy.Id == id || insurancePolicy.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result> Add(InsurancePolicy insurancePolicy)
        {
            Result result = new Result() { IsSuccess = true, Message = "Message" };

            try
            {
                await _context.InsurancePolicy.InsertOneAsync(insurancePolicy);

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
                DeleteResult actionResult = await _context.InsurancePolicy.DeleteOneAsync(Builders<InsurancePolicy>.Filter.Eq("Id", id));
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "InsurancePolicy Record deleted";
               
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        //public async Task<bool> UpdateBorrower(string id, Borrower borrower)
        //{
        //    var filter = Builders<Borrower>.Filter.Eq(s => s.Id, id);
        //    var update = Builders<Borrower>.Update.Set(s => s.Comments, borrower.Comments).CurrentDate(s => s.UpdatedOn);

        //    try
        //    {
        //        UpdateResult actionResult = await _context.Borrower.UpdateOneAsync(filter, update);

        //        return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        // log or manage the exception
        //        throw ex;
        //    }
        //}

        public async Task<Result> Update(string id, InsurancePolicy insurancePolicy)
        {
            Result result = new Result();
            try
            {
                //var filter = Builders<InsurancePolicy>.Filter.Eq(s => s.Id, id);
                //var update = Builders<InsurancePolicy>.Update.Set(s => s.Comments, insurancePolicy.).CurrentDate(s => s.UpdatedOn);

                //UpdateResult actionResult = await _context.Borrower.UpdateOneAsync(filter, update);

                //ReplaceOneResult actionResult = await _context.InsurancePolicy.ReplaceOneAsync(n => n.Id.Equals(id),
                //                                                                      insurancePolicy,
                //                                                                      new UpdateOptions { IsUpsert = true });
                //result.IsSuccess = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                //result.Message = "InsurancePolicy Updated";
               
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
                DeleteResult actionResult = await _context.InsurancePolicy.DeleteManyAsync(new BsonDocument());
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "InsurancePolicie All records removed";             
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

        public Task<Loan> GetLoanDetails()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetPropertyDetails()
        {
            throw new NotImplementedException();
        }
    }
}
