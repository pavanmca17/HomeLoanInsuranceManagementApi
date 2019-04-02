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
            Result result = new Result() { IsSuccess = true, Message = "Message" };
           
            try
            {
                await _context.Banks.InsertOneAsync(bank);
               
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;   
            }

            return await Task.FromResult<Result>(result);
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Banks.DeleteOneAsync(Builders<Bank>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Bank bank)
        {
            var filter = Builders<Bank>.Filter.Eq(s => s.Id, id);
            var update = Builders<Bank>.Update.Set(s => s.Comments, bank.Comments).CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult = await _context.Banks.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateBank(string id, Bank bank)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Banks.ReplaceOneAsync(n => n.Id.Equals(id),
                                                                                      bank,
                                                                                      new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Cleanup
        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult = await _context.Banks.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
