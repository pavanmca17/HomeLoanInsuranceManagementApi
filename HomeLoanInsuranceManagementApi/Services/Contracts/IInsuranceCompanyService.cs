using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IInsuranceCompanyService : IService<InsuranceCompany>
    {
        Task<List<InsurancePolicy>> GetInsurancePoliciesDetails();

        Task<List<Loan>> GetLoansDetails();

        Task<List<Property>> GetPropertiesDetails();

        Task<List<Borrower>> GetBorrowersDetails();


    }
}
