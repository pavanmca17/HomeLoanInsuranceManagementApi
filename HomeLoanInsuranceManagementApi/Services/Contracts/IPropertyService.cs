using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IPropertyService :  IService<Property>
    {
        // Methods are Querying Property Company Entity related information

        Task<Borrower> GetBorrowerDetails();

        Task<Loan> GetLoanDetails();       

        Task<List<InsurancePolicy>> GetAllPolicies();
    }
}
