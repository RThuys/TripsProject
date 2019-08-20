using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backend.Data.Models;
using backend.Data.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : Controller
    {
        private readonly ITripRepository _repository;

        public TripsController(ITripRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            IEnumerable<Trip> trips = await _repository.GetAllTrips();

            return Ok(trips);
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var trip = await _repository.GetTripById(id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // GET: api/Trips/past
        [HttpGet("past")]
        public async Task<IActionResult> GetTripsPast([FromRoute] string test)
        {
            IEnumerable<Trip> trips = await _repository.GetAllTripsPast();

            return Ok(trips);
        }

        // GET: api/Trips/future
        [HttpGet("future")]
        public async Task<IActionResult> GetTripsFuture([FromRoute] string test)
        {
            IEnumerable<Trip> trips = await _repository.GetAllTripsFuture();

            return Ok(trips);
        }

        // GET: api/Trips/today
        [HttpGet("today")]
        public async Task<IActionResult> GetTripsToday([FromRoute] string test)
        {
            IEnumerable<Trip> trips = await _repository.GetAllTripsToday();

            return Ok(trips);
        }

        // POST: api/Trips
        [HttpPost]
        //public IActionResult Post([FromBody] Trip json )
        public async Task<IActionResult> Post([FromBody] Trip json)
        {
            if (json == null)
            {
                return BadRequest();
            }

            try
            {
                var newTrip = await _repository.AddTrip(json);
                return Ok(newTrip);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpDelete("{id}", Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _repository.RemoveTrip(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
