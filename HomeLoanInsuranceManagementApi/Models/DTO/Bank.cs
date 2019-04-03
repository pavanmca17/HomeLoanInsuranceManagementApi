using System;
using System.Collections.Generic;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Bank : BaseEntity
    {   
       public List<string> LoanIds { get; set; }

       public List<string> BorrowerIds { get; set; }

       public List<string> PropertiesIds { get; set; }

    }
}
