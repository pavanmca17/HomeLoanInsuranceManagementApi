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
    public class BanksController : Controller
    {
        private readonly IBankService _bankService;

        public BanksController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        [Route("api/Banks/")]
        public async Task<IEnumerable<Bank>> Get()
        {
            return await _bankService.GetAll();
        }

        [HttpGet()]
        [Route("api/Banks/bank/{id}")]
        public async Task<Bank> Get(string id)
        {
            return await _bankService.Get(id) ?? new Bank();
        }

        [HttpPost]
        [Route("api/Banks/Create")]
        public async Task<ActionResult<Result>> Create(Bank bank)
        {

            var result = await  _bankService.Add(new Bank
            {
                Id = Guid.NewGuid().ToString(),
                Name = bank.Name,
                CreatedUsername = bank.CreatedUsername,
                CreatedOn = DateTime.Now,
                UpdatedUsername = null,
                UpdatedOn = default(DateTime),
                Comments = "Bank Entity Created"
            });
              
            return Ok(result);
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string comments)
        {
            Bank bank = new Bank();
            bank.Comments = comments;
            _bankService.Update(id, bank);
        }

        [HttpDelete()]
        public void Delete()
        {
            _bankService.RemoveAll();
        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _bankService.Remove(id);
        }
    }
}