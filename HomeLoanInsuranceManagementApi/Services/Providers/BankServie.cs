using HomeLoanInsuranceManagementApi.DataBaseContext;
using HomeLoanInsuranceManagementApi.Models;
using HomeLoanInsuranceManagementApi.Services.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Providers
{
    public class BankServie : IBankService
    {
        private readonly MongoDBContext _context = null;

        public BankServie(IOptions<Settings> settings)
        {
            _context = new MongoDBContext(settings);
        }

        // Get all Banks
        public async Task<IEnumerable<Bank>> GetAll()
        {
            try
            {
                return await _context.Banks.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        public async Task<Bank> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Banks.Find(note => note.Id == id || note.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public async Task<Result> Add(Bank bank)
        {
            Result result = new Result(); 
           
            try
            {
                await _context.Banks.InsertOneAsync(bank);
                result.IsSuccess = true;
                result.Message = "Bank Created Successfully";              

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
                DeleteResult actionResult = await _context.Banks.DeleteOneAsync(Builders<Bank>.Filter.Eq("Id", id));
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "Bank Record Deleted";
                
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        public async Task<Result> Update(string id, Bank bank)
        {
            Result result = new Result();
            try
            {
                var filter = Builders<Bank>.Filter.Eq(s => s.Id, id);
                var update = Builders<Bank>.Update.Set(s => s.BorrowerIds, bank.BorrowerIds)
                                                  .Set(s => s.PropertiesIds, bank.PropertiesIds)
                                                  .Set(s => s.LoanIds, bank.LoanIds)
                                                  .Set(s => s.UpdateComments, bank.UpdateComments)
                                                  .Set(s => s.UpdatedUsername, bank.UpdatedUsername)
                                                  .CurrentDate(s => s.UpdatedOn);

                UpdateResult actionResult = await _context.Banks.UpdateOneAsync(filter, update);

                result.IsSuccess = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;

                result.Message = "Bank Record Updated";
                
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
                DeleteResult actionResult = await _context.Banks.DeleteManyAsync(new BsonDocument());

                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "Deleted all Bank records";                
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

        Task<List<InsurancePolicy>> IBankService.GetAllPolicies()
        {
            throw new NotImplementedException();
        }

        Task<List<Borrower>> IBankService.GetAllBorrowers()
        {
            throw new NotImplementedException();
        }

        public Task<List<Loan>> GetAllLoans()
        {
            throw new NotImplementedException();
        }

        public Task<List<Property>> GetAllProperties()
        {
            throw new NotImplementedException();
        }
    }
}
