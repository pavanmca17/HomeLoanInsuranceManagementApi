using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Loan
    {
        public Loan()
        {

        }

        public Double OriginalAmount { get; set; }

        public Double RemainingAmount { get; set; }

        public Double OriginalTenure { get; set; }

        public Double RemainingTenure { get; set; }

        
    }
}
