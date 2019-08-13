using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backend.Data.Models;
using backend.Models;

using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _appDbContext;

        public TripRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Trip>> GetAllTrips()
        {
            return await _appDbContext.Trips.ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetAllTripsPast()
        {
            string query = "select * from Trips where DATE <= GETDATE();";
            return await _appDbContext.Trips.FromSql(query).ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetAllTripsFuture()
        {
            string query = "select * from Trips where DATE >= GETDATE();";
            return await _appDbContext.Trips.FromSql(query).ToListAsync();
        }

        public async Task<Trip> GetTripById(int tripId)
        {
            var returnable = _appDbContext.Trips.Where(t => t.Id == tripId);
            if (returnable == null)
            {
                return null;
            }

            return await returnable
                .Select (trip => new Trip
                    {
                        Id = trip.Id,
                        Title = trip.Title,
                        SupervisorId = trip.SupervisorId,
                        Date = trip.Date
                    }
                )
                .FirstOrDefaultAsync();
        }

        public async Task<Trip> AddTrip(Trip Trip)
        {

            var returnable = _appDbContext.Trips.Add(Trip);
         
            if (_appDbContext.Supervisors.Find (Trip.SupervisorId) == null)
            {
                throw new KeyNotFoundException("This supervisor does not exist");
            }

            await _appDbContext.SaveChangesAsync();
            return returnable.Entity;

        }

        public async Task RemoveTrip(int id) {
            var trip = _appDbContext.Trips.Find(id);

            if (trip == null)
            {
                throw new KeyNotFoundException();
            }

            _appDbContext.Trips.Remove(trip);

            await _appDbContext.SaveChangesAsync();
        }

        
    }
}
