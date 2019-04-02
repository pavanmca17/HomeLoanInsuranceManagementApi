using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IService<T> where T : class
    {

        Task<T> Get(string id);

        Task<IEnumerable<T>> GetAll();

        Task<Result> Add(T t);

        Task<bool> Update(string id, T t);

        Task<bool> Remove(string id);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAll();

        // demo interface - full document update
        //Task<bool> UpdateDocument(string id, string body);

        // query after multiple parameters
        //  Task<IEnumerable<Bank>> GetBanks(string bodyText, DateTime updatedFrom, long headerSizeLimit);

    }
}
