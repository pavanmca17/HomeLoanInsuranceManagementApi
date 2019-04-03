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

        Task<Result> Update(string id, T t);

        Task<Result> Remove(string id);

        Task<Result> RemoveAll();

            

    }
}
