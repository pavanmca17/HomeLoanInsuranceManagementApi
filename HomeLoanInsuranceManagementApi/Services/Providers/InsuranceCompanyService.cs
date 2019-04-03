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
    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        private readonly MongoDBContext _context = null;

        public InsuranceCompanyService(IOptions<Settings> settings)
        {
            _context = new MongoDBContext(settings);
        }

        // Get all Banks
        public async Task<IEnumerable<InsuranceCompany>> GetAll()
        {
            try
            {
                return await _context.InsuranceCompany.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        public async Task<InsuranceCompany> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.InsuranceCompany.Find(insuranceCompany => insuranceCompany.Id == id || insuranceCompany.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result> Add(InsuranceCompany insuranceCompany)
        {
            Result result = new Result();

            try
            {
                await _context.InsuranceCompany.InsertOneAsync(insuranceCompany);
                result.IsSuccess = true;
                result.Message = "InsuranceCompany Created";

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
                DeleteResult actionResult = await _context.InsuranceCompany.DeleteOneAsync(Builders<InsuranceCompany>.Filter.Eq("Id", id));
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "InsuranceCompany Removed";
               
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

        public async Task<Result> Update(string id, InsuranceCompany insuranceCompany)
        {
            Result result = new Result();
            try
            {
                ReplaceOneResult actionResult = await _context.InsuranceCompany.ReplaceOneAsync(n => n.Id.Equals(id),
                                                                                      insuranceCompany,
                                                                                      new UpdateOptions { IsUpsert = true });
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                result.Message = "InsuranceCompany Update";
              
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
                DeleteResult actionResult = await _context.InsuranceCompany.DeleteManyAsync(new BsonDocument());
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "All InsuranceCompanies removed";                 
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

        public Task<List<InsurancePolicy>> GetInsurancePoliciesDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<Loan>> GetLoansDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<Property>> GetPropertiesDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<Borrower>> GetBorrowersDetails()
        {
            throw new NotImplementedException();
        }
    }
}
