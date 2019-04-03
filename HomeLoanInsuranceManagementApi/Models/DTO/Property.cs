using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Property : BaseEntity
    {
        public String BankID { get; set; }

        public String LoanID { get; set; }

        public String borrowerId { get; set; }

        public String currentPolicyID { get; set; }

        public List<String> previouspoliciesIds { get; set; }


    }
}
