using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class InsuranceCompany : BaseEntity
    {
       public List<String> policiesIds { get; set;}
    }
}
