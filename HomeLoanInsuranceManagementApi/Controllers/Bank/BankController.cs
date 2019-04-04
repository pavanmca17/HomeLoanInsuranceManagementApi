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
        public async Task<ActionResult<IEnumerable<Bank>>> GetAllBanks()
        {
           
            var banks = await _bankService.GetAll();
            return Ok(banks);
        }

        [HttpGet()]
        [Route("api/Banks/{id}")]
        public async Task<ActionResult<Bank>> GetBank(string id)
        {
            var bank = await _bankService.Get(id) ?? new Bank();
            return Ok(bank);
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
                Comments = "Bank Entity Creation"                
            });
              
            return Ok(result);
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut()]
        [Route("api/Banks/Update/{id}")]
        public async Task<ActionResult<Result>> Put(string id, Bank bank)
        {
            var result = await _bankService.Update(id, bank);
            return Ok(result);
        }

        [HttpDelete()]
        [Route("api/Banks/DeleteAll")]
        public async Task<ActionResult<Result>> Delete()
        {
            var result = await _bankService.RemoveAll();
            return Ok(result);
        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete]
        [Route("api/Banks/Delete/{id}")]
        public async Task<ActionResult<Result>> Delete(string id)
        {
            var result = await _bankService.Remove(id);
            return Ok(result);
        }
    }
}