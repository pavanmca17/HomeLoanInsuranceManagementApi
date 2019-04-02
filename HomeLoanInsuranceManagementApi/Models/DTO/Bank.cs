using System;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Bank : BaseEntity
    {   
       public List<string> borrowerIds { get; set; }
       public List<string> propertiesIds { get; set; }
    }
}
