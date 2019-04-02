using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class InsurancePolicy
    {
        
        string Issuedby { get; set; }

        [BsonDateTimeOptions]
        public DateTime PolicyStartDate { get; set; }

        [BsonDateTimeOptions]
        public DateTime PolicyEndDate { get; set; }

        public bool IsActive { get; set; }





    }
}
