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
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;

        }

        [HttpGet]
        [Route("api/Properties/")]
        public async Task<IEnumerable<Property>> GetAllProperties()
        {
            return await _propertyService.GetAll();
        }

        [HttpGet()]
        [Route("api/Properties/{id}")]
        public async Task<Property> GetInsurancePolicy(string id)
        {
            return await _propertyService.Get(id) ?? new Property();
        }

        [HttpPost]
        [Route("api/Properties/Create")]
        public async Task<ActionResult<Result>> Create(Property property)
        {

            var result = await _propertyService.Add(new Property
            {
                Id = Guid.NewGuid().ToString(),
                Name = property.Name,
                BankID = property.BankID,
                borrowerId = property.borrowerId,                
                CreatedUsername = property.CreatedUsername,
                CreatedOn = DateTime.Now,
                UpdatedUsername = null,
                UpdatedOn = default(DateTime),
                Comments = "Property Entity Creation"                
            });

            return Ok(result);
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut()]
        [Route("api/Properties/Update/{id}")]
        public void Put(string id, Property property)
        {
            _propertyService.Update(id, property);
        }

        [HttpDelete()]
        [Route("api/Properties/Delete")]
        public void Delete()
        {
            _propertyService.RemoveAll();

        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete("{id}")]
        [Route("api/Properties/Delete/{id}")]
        public void Delete(string id)
        {
            _propertyService.Remove(id);

        }
    }
}