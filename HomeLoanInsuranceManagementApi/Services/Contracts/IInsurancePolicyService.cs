using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IInsurancePolicyService : IService<InsurancePolicy>
    {
        // Methods are Querying Insurance Policy Entity related information

        Task<Loan> GetLoanDetails();

        Task<Property> GetPropertyDetails();
        
    }
}
