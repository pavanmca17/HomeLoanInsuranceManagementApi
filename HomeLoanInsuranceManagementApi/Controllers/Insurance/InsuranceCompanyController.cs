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
    public class InsuranceCompanyController : Controller
    {
        private readonly IInsuranceCompanyService _insuranceCompanyService;

        public InsuranceCompanyController(IInsuranceCompanyService insuranceCompanyService)
        {
            _insuranceCompanyService = insuranceCompanyService;
        }

        [HttpGet]
        [Route("api/InsuranceCompanies/")]
        public async Task<ActionResult<IEnumerable<InsuranceCompany>>> GetAllInsuranceCompanies()
        {
            var insuranceCompanies = await _insuranceCompanyService.GetAll();
            return Ok(insuranceCompanies);
        }

        [HttpGet()]
        [Route("api/InsuranceCompanies/{id}")]
        public async Task<ActionResult<InsuranceCompany>> GetInsuranceCompany(string id)
        {
            var insuranceCompany = await _insuranceCompanyService.Get(id) ?? new InsuranceCompany();
            return Ok(insuranceCompany);
        }

        [HttpPost]
        [Route("api/InsuranceCompanies/Create")]
        public async Task<ActionResult<Result>> Create(InsuranceCompany insuranceCompany)
        {

            var result = await _insuranceCompanyService.Add(new InsuranceCompany
            {
                Id = Guid.NewGuid().ToString(),
                Name = insuranceCompany.Name,                
                CreatedUsername = insuranceCompany.CreatedUsername,
                CreatedOn = DateTime.Now,
                UpdatedUsername = null,
                UpdatedOn = default(DateTime),
                Comments = "InsuranceCompany Entity Creation"                
            });

            return Ok(result);
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut]
        [Route("api/InsuranceCompanies/Update/{id}")]
        public async Task<ActionResult<Result>> Put(string id, InsuranceCompany insuranceCompany)
        {
            var result = await _insuranceCompanyService.Update(id, insuranceCompany);
            return Ok(result);
        }

        [HttpDelete()]
        [Route("api/InsuranceCompanies/Delete")]
        public async Task<ActionResult<Result>> Delete()
        {
            var result = await _insuranceCompanyService.RemoveAll();
            return Ok(result);
        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete()]
        [Route("api/InsuranceCompanies/Delete/{id}")]
        public async Task<ActionResult<Result>> Delete(string id)
        {
            var result = await _insuranceCompanyService.Remove(id);
            return Ok(result);
        }
    }
}