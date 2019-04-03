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
    public class PropertyService : IPropertyService
    {
        private readonly MongoDBContext _context = null;

        public PropertyService(IOptions<Settings> settings)
        {
            _context = new MongoDBContext(settings);
        }

        // Get all Banks
        public async Task<IEnumerable<Property>> GetAll()
        {
            try
            {
                return await _context.Property.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        public async Task<Property> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Property.Find(Property => Property.Id == id || Property.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result> Add(Property property)
        {
            Result result = new Result();

            try
            {
                await _context.Property.InsertOneAsync(property);
                result.IsSuccess = true;
                result.Message = "Property Record added";
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
                DeleteResult actionResult = await _context.Property.DeleteOneAsync(Builders<Property>.Filter.Eq("Id", id));
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "Property record deleted";                 
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }

            return await Task.FromResult<Result>(result);
        }

        public async Task<Result> Update(string id, Property property)
        {
            Result result = new Result();
            try
            {
                var filter = Builders<Property>.Filter.Eq(s => s.Id, id);
                var update = Builders<Property>.Update.Set(s => s.currentPolicyID, property.currentPolicyID)
                                                      .Set(s => s.previouspoliciesIds, property.previouspoliciesIds)
                                                      .CurrentDate(s => s.UpdatedOn);

                UpdateResult actionResult = await _context.Property.UpdateOneAsync(filter, update);

                //ReplaceOneResult actionResult = await _context.Property.ReplaceOneAsync(n => n.Id.Equals(id),
                //                                                                      property,
                //                                                                      new UpdateOptions { IsUpsert = true });
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                result.Message = "Property Updated";
              
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
                DeleteResult actionResult = await _context.Property.DeleteManyAsync(new BsonDocument());
                result.IsSuccess = actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
                result.Message = "All Properties Records Deleted";                
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

        public Task<Loan> GetLoanDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<InsurancePolicy>> GetAllPolicies()
        {
            throw new NotImplementedException();
        }
    }
}
