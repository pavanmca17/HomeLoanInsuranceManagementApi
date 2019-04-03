using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Loan : BaseEntity
    {
        public String BankId { get; set; }
        
        public String PropertyId { get; set; }
        
        public String BorrowerId { get; set;}

        public String CurrentInsurancePolicyID { get; set; }

        public Double OriginalAmount { get; set; }

        public Double RemainingAmount { get; set; }

        public Double OriginalTenure { get; set; }

        public Double RemainingTenure { get; set; }

        public List<String> PreviousPoliciesIds { get; set;}
    }
}
