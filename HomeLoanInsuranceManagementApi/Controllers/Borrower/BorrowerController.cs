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
    public class BorrowerController : Controller
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowerController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        [Route("api/Borrowers/")]
        public async Task<ActionResult<IEnumerable<Borrower>>> GetAllBorrowers()
        {
            var borrowers = await _borrowerService.GetAll();
            return Ok(borrowers);
        }

        [HttpGet()]
        [Route("api/Borrowers/Borrower/{id}")]
        public async Task<ActionResult<Borrower>> GetBorrower(string id)
        {
            var borrowers = await _borrowerService.Get(id) ?? new Borrower();
            return Ok(borrowers);
        }

        [HttpPost]
        [Route("api/Borrowers/Create")]
        public async Task<ActionResult<Result>> Create(Borrower borrower)
        {

            var result = await _borrowerService.Add(new Borrower
            {
                Id = Guid.NewGuid().ToString(),
                Name = borrower.FirstName + borrower.MiddleName + borrower.LastName,
                FirstName = borrower.FirstName,
                MiddleName = borrower.MiddleName,
                LastName = borrower.LastName,
                Email = borrower.Email,
                gender = borrower.gender,
                MailingAddress = borrower.MailingAddress,
                StreetAddress = borrower.StreetAddress,
                Phone = borrower.Phone,                
                CreatedUsername = borrower.CreatedUsername,
                CreatedOn = DateTime.Now,
                UpdatedUsername = null,
                UpdatedOn = default(DateTime),
                Comments = "Borrower Entity Creation" 
            });

            return Ok(result);
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut()]
        [Route("api/Borrowers/Update/{id}")]
        public async Task<ActionResult<Result>> Put(string id, Borrower borrower)
        {
           var result = await _borrowerService.Update(id, borrower);
           return Ok(result);
        }

        [HttpDelete()]
        [Route("api/Borrowers/Delete")]
        public async Task<ActionResult<Result>> Delete()
        {
            var result = await _borrowerService.RemoveAll();
            return Ok(result);
        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete]
        [Route("api/Borrowers/Delete/{id}")]
        public async Task<ActionResult<Result>> Delete(string id)
        {
            var result = await _borrowerService.Remove(id);
            return Ok(result);
        }
    }
}