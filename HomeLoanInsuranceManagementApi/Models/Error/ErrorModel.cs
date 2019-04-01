using System;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class ErrorModel
    {
        public ErrorModel()
        {
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}