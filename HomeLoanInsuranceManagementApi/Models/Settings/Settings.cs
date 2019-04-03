using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLoanInsuranceManagementApi.Models
{
    public class Settings
    {
        public string MongoDBConnectionString;
        public string MongoDatabase;
        public string Env;
        public bool RequestResponseLogging;

    }    
}
