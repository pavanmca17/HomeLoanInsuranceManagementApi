using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IBankService
    {
        Task<IEnumerable<Bank>> GetAllNotes();

        Task<Bank> GetNote(string id);

        // query after multiple parameters
      //  Task<IEnumerable<Bank>> GetBanks(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        // add new note document
        Task AddBank(Bank bank);

        // remove a single document / note
        Task<bool> RemoveBank(string id);

        // update just a single document / note
        Task<bool> UpdateBank(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateBankDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllBanks();
    }
}
