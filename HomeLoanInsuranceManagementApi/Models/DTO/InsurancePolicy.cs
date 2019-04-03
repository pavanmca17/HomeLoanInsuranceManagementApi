using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class InsurancePolicy : BaseEntity
    {
        public string PropertyId { get; set; }

        public string BorrowerId { get; set; }

        [BsonDateTimeOptions]
        public DateTime PolicyStartDate { get; set; }

        [BsonDateTimeOptions]
        public DateTime PolicyEndDate { get; set; }

        public bool IsActive { get; set; }

        public Double InsuredAmount { get; set; }        

    }
}
