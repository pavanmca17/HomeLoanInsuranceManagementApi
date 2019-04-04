using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface ILoanService : IService<Loan>
    {
        // Methods are Querying Loan Company Entity related information

        Task<Borrower> GetBorrowerDetails();

        Task<Property> GetPropertyDetails();        

        Task<List<InsurancePolicy>> GetInsurancePoliciesDetails();
       
    }
}
