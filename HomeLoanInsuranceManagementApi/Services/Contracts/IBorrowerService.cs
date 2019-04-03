﻿using HomeLoanInsuranceManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Services.Contracts
{
    public interface IBorrowerService : IService<Borrower>
    {
        Task<Property> GetPropertyDetails();

        Task<Loan> GetLoanDetails();

        Task<List<InsurancePolicy>> GetInsurancePoliciesDetails();

        // persuming a Single borrow can own Multiple Properties and Loans 
        Task<List<Property>> GetPropertiesDetails();

        Task<List<Loan>> GetLoansDetails();
    }
}
