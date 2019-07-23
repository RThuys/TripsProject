using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Models;
using backend.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorsController : ControllerBase
    {

        private readonly ISupervisorRepository _repo;

        public SupervisorsController(ISupervisorRepository repo)
        { _repo = repo; }

        // GET: api/Supervisor
        [HttpGet]
        public async Task<IActionResult> GetSupervisors()
        {
            return Ok(await _repo.GetAllSupervisor());
        }

        // GET: api/Supervisor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
           if (id == 0)
            {
                return BadRequest();
            }
            Supervisor supervisor = await _repo.GetSupervisorById(id);

            if (supervisor == null)
            {
                return NotFound();
            }
            return Ok(supervisor);
        }

        // POST: api/Supervisor
        [HttpPost]
        public async Task<IActionResult> PostSupervisor([FromBody] Supervisor supervisor)
        {
            if(supervisor == null)
            {
                return BadRequest();
            }
            var newSupervisor = await _repo.AddSupervisor(supervisor);
            return Ok(newSupervisor);
        }

        // PUT: api/Supervisor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
