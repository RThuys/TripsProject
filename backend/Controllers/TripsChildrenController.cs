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
    public class TripsChildrenController : ControllerBase
    {
        private readonly ITripChildRepository _repo;

        public TripsChildrenController(ITripChildRepository repo)
        {
            _repo = repo;
        }

        // GET: api/TripsChildren
        [HttpGet]
        public async Task<IActionResult> GetTripsChildren()
        {
            return Ok(await _repo.GetAllTripsChildren());
        }

        // GET: api/TripsChildren/5
        [HttpGet("{id}")]
        public Task<IActionResult> Get([FromRoute] int id)
        {
            return null;
        }

        // POST: api/TripsChildren
        [HttpPost]
        public async Task<IActionResult> PostTripChild([FromBody] TripChild tripChild)
        {
            if (tripChild == null)
            {
                return BadRequest();
            }

            var newChild = await _repo.AddTripChild(tripChild);
            return Ok(newChild);
        }

        // PUT: api/TripsChildren/5
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
