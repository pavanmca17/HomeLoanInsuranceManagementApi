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
        public async Task<IEnumerable<Bank>> GetAllBanks()
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
        public async Task<Bank> GetNote(string id)
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

        //// query after body text, updated time, and header image size
        //public async Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        //{
        //    try
        //    {
        //        var query = _context.Notes.Find(note => note.Body.Contains(bodyText) &&
        //                                        note.UpdatedOn >= updatedFrom &&
        //                                        note.HeaderImage.ImageSize <= headerSizeLimit);

        //        return await query.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // log or manage the exception
        //        throw ex;
        //    }
        //}

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddBank(Bank bank)
        {
            try
            {
                await _context.Banks.InsertOneAsync(bank);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveBank(string id)
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

        public async Task<bool> UpdateBank (string id, string body)
        {
            var filter = Builders<Bank>.Filter.Eq(s => s.Id, id);
            var update = Builders<Bank>.Update.Set(s => s.Body, body).CurrentDate(s => s.UpdatedOn);

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

        // Demo function - full document update
        public async Task<bool> UpdateBankDocument(string id, string body)
        {
            var item = await GetNote(id) ?? new Bank();
            item.Body = body;
            item.UpdatedOn = DateTime.Now;
            return await UpdateBank(id, item);
        }

        // Cleanup
        public async Task<bool> RemoveAllBanks()
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
    }
}
s