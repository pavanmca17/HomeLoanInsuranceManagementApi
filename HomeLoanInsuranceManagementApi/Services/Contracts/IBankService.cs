using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IBankService : IService<Bank>
    {
        Task<List<Loan>> GetAllLoans();

        Task<List<Property>> GetAllProperties();

        Task<List<Borrower>> GetAllBorrowers();

        Task<List<InsurancePolicy>> GetAllPolicies();
    }
}
