using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Borrower : BaseEntity
    {
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String gender { get; set; }
        public String Email { get; set;  }
        public String Phone { get; set; }
        public String StreetAddress { get; set; }
        public String MailingAddress { get; set; }
        public List<string> propertiesIds { get; set; }

    }
}
