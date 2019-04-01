using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeLoanInsuranceManagementApi.Models;
using HomeLoanInsuranceManagementApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HomeLoanInsuranceManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class BanksController : Controller
    {
        private readonly IBankService _bankService;

        public BanksController(IBankService bankService)
        {
            _bankService = bankService;
        }


        [HttpGet]
        public async Task<IEnumerable<Bank>> Get()
        {
            return await _bankService.GetAllNotes();
        }

        // GET api/notes/5 - retrieves a specific note using either Id or InternalId (BSonId)
        [HttpGet("{id}")]
        public async Task<Bank> Get(string id)
        {
            return await _bankService.GetNote(id) ?? new Bank();
        }

        // GET api/notes/text/date/size
        // ex: http://localhost:53617/api/notes/Test/2018-01-01/10000
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<Bank>> Get(string bodyText,
                                                 DateTime updatedFrom,
                                                 long headerSizeLimit)
        {
            return await _bankService.GetNote(bodyText, updatedFrom, headerSizeLimit)
                        ?? new List<Bank>();
        }

        // POST api/notes - creates a new note
        [HttpPost]
        public void Post([FromBody] NoteParam newNote)
        {
            _noteRepository.AddNote(new Note
            {
                Id = newNote.Id,
                Body = newNote.Body,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                UserId = newNote.UserId
            });
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _noteRepository.UpdateNoteDocument(id, value);
        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepository.RemoveNote(id);
        }
    }
}