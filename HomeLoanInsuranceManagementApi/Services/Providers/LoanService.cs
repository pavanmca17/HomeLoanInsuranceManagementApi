
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
    public class LoanService : ILoanService
    {
        private readonly MongoDBContext _context = null;

        public LoanService(IOptions<Settings> settings)
        {
            _context = new MongoDBContext(settings);
        }

        // Get all Banks
        public async Task<IEnumerable<Loan>> GetAll()
        {
            try
            {
                return await _context.Loan.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        public async Task<Loan> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Loan.Find(loan => loan.Id == id || loan.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result> Add(Loan loan)
        {
            Result result = new Result();

            try
            {
                await _context.Loan.InsertOneAsync(loan);
                result.IsSuccess = true;
                result.Message = "Loan Record added";

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
                DeleteResult actionResult = await _context.Loan.DeleteOneAsync(Builders<Loan>.Filter.Eq("Id", id));
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "Loan Record Deleted";                 
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

        public async Task<Result> Update(string id, Loan loan)
        {
            Result result = new Result();

            try
            {
                var filter = Builders<Loan>.Filter.Eq(s => s.Id, id);
                var update = Builders<Loan>.Update.Set(s => s.CurrentInsurancePolicyID, loan.CurrentInsurancePolicyID)
                                                   .Set(s => s.PreviousPoliciesIds, loan.PreviousPoliciesIds)
                                                   .CurrentDate(s => s.UpdatedOn);

                UpdateResult actionResult = await _context.Loan.UpdateOneAsync(filter, update);

                result.IsSuccess = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                result.Message = "Loan Record Updated";
               
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
                DeleteResult actionResult = await _context.Loan.DeleteManyAsync(new BsonDocument());
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "All Loan records deleted";              
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

        public Task<Borrower> GetBorrowerDetails()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetPropertyDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<InsurancePolicy>> GetInsurancePoliciesDetails()
        {
            throw new NotImplementedException();
        }
    }
}
