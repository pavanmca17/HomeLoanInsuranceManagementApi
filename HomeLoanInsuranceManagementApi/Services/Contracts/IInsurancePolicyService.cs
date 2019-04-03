using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IInsurancePolicyService : IService<InsurancePolicy>
    {
        Task<Loan> GetLoanDetails();

        Task<Property> GetPropertyDetails();
        
    }
}
