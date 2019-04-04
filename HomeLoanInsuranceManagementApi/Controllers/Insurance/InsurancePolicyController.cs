using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeLoanInsuranceManagementApi.Models;
using HomeLoanInsuranceManagementApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HomeLoanInsuranceManagementApi.Controllers
{
        [ApiVersion("2.0")]
        [ApiController]
        public class InsurancePolicyController : Controller
        {
            private readonly IInsurancePolicyService _insurancePolicyService;

            public InsurancePolicyController(IInsurancePolicyService insurancePolicyService)
            {
                _insurancePolicyService = insurancePolicyService;

            }

            [HttpGet]
            [Route("api/InsurancePolicies/")]
            public async Task<ActionResult<IEnumerable<InsurancePolicy>>> GetAllInsurancePolicies()
            {
                var insurancePolicies = await _insurancePolicyService.GetAll();
                return Ok(insurancePolicies);
            }

            [HttpGet()]
            [Route("api/InsurancePolicies/{id}")]
            public async Task<ActionResult<InsurancePolicy>> GetInsurancePolicy(string id)
            {
                var insurancePolicy = await _insurancePolicyService.Get(id) ?? new InsurancePolicy();
                return Ok(insurancePolicy);
            }

            [HttpPost]
            [Route("api/InsurancePolicies/Create")]
            public async Task<ActionResult<Result>> Create(InsurancePolicy insurancePolicy)
            {

                var result = await _insurancePolicyService.Add(new InsurancePolicy
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = insurancePolicy.Name,
                    BorrowerId = insurancePolicy.BorrowerId,
                    InsuredAmount = insurancePolicy.InsuredAmount,
                    PolicyStartDate = insurancePolicy.PolicyStartDate,
                    PolicyEndDate = insurancePolicy.PolicyEndDate,
                    PropertyId = insurancePolicy.PropertyId,
                    IsActive = true,                    
                    CreatedUsername = insurancePolicy.CreatedUsername,
                    CreatedOn = DateTime.Now,
                    UpdatedUsername = null,
                    UpdatedOn = default(DateTime),
                    Comments = "Insurance Policy Entity Creation" 
                });

                return Ok(result);
            }

            // PUT api/notes/5 - updates a specific note
            [HttpPut()]
            [Route("api/InsurancePolicies/Update/{id}")]
            public async Task<ActionResult<Result>> Put(string id, InsurancePolicy insurancePolicy)
            {
               var result = await _insurancePolicyService.Update(id, insurancePolicy);
               return Ok(result);
            }

            [HttpDelete()]
            [Route("api/InsurancePolicies/Delete")]
            public async Task<ActionResult<Result>> Delete()
            {
                var result = await _insurancePolicyService.RemoveAll();
                return Ok(result);

            }

            // DELETE api/notes/5 - deletes a specific note
            [HttpDelete("{id}")]
            [Route("api/InsurancePolicies/Delete/{id}")]
            public async Task<ActionResult<Result>> Delete(string id)
            {
               var result = await _insurancePolicyService.Remove(id);
               return Ok(result);

            }
        }
    }
