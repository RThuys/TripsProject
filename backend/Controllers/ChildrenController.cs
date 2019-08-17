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
    public class ChildrenController : ControllerBase
    {

        private readonly IChildRepository _repo;

        public ChildrenController(IChildRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Children
        [HttpGet]
        public async Task<IActionResult> GetChildren()
        {
            return Ok(await _repo.GetAllChildren());
        }

        // GET: api/Children/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Child child = await _repo.GetChildById(id);

            if (child == null )
            {
                return NotFound();
            }
            return Ok(child);
        }

        // POST: api/Children
        [HttpPost]
        public async Task<IActionResult> PostChild([FromBody] Child child)
        {
           if (child == null)
            {
                return BadRequest();
            }

            var newChild = await _repo.AddChild(child);
            return Ok(newChild);
        }


        // PUT: api/Children
        [HttpPut]
        public async Task<IActionResult> PutChild([FromBody] Child child)
        {
            if (child == null)
            {
                return BadRequest();
            }

            var updatedChild = await _repo.UpdateChild(child);
            return Ok(updatedChild);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _repo.RemoveChild(id);
                return Ok();
            } catch (KeyNotFoundException message)
            {
                return NotFound(message.Message);
            }
        }
    }
}
