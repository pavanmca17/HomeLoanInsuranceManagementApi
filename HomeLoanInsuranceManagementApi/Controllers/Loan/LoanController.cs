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
        public class LoanController : Controller
        {
            private readonly ILoanService _loanService;

            public LoanController(ILoanService loanService)
            {
                _loanService = loanService;

            }

            [HttpGet]
            [Route("api/Loans/")]
            public async Task<ActionResult<IEnumerable<Loan>>> GetAllLoans()
            {
                var loans = await _loanService.GetAll();
                return Ok(loans);
            }

            [HttpGet()]
            [Route("api/Loans/{id}")]
            public async Task<Loan> GetLoan(string id)
            {
                return await _loanService.Get(id) ?? new Loan();
            }

            [HttpPost]
            [Route("api/Loans/Create")]
            public async Task<ActionResult<Result>> Create(Loan loan)
            {

                var result = await _loanService.Add(new Loan
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = loan.Name,
                    BankId = loan.BankId,
                    BorrowerId = loan.BorrowerId,
                    OriginalAmount = loan.OriginalAmount,
                    OriginalTenure = loan.OriginalTenure,
                    PropertyId = loan.PropertyId,                    
                    CreatedUsername = loan.CreatedUsername,
                    CreatedOn = DateTime.Now,
                    UpdatedUsername = null,
                    UpdatedOn = default(DateTime),
                    Comments = "Loan Entity Creation"                    
                });

                return Ok(result);
            }

            // PUT api/notes/5 - updates a specific note
            [HttpPut()]
            [Route("api/Loans/Update/{id}")]
            public async Task<ActionResult<Result>> Put(string id, Loan loan)
            {
                var result = await _loanService.Update(id, loan);
                return Ok(result);
            }

            [HttpDelete()]
            [Route("api/Loans/Delete")]
            public async Task<ActionResult<Result>> Delete()
            {
                var result = await _loanService.RemoveAll();
                return Ok(result);

            }

            // DELETE api/notes/5 - deletes a specific note
            [HttpDelete("{id}")]
            [Route("api/Loans/Delete/{id}")]
            public async Task<ActionResult<Result>> Delete(string id)
            {
               var result = await _loanService.Remove(id);
               return Ok(result);

            }
        }
    }
