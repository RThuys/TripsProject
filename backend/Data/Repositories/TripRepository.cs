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
                       // Children = trip.TripChildren.Select (tt => tt.Child).ToList(),
                       // Scans = trip.Scans.Select( scan => new Scan
                       //    {
                       //      Id= scan.Id,
                       //    Name = scan.Name,
                       //    TripId = scan.TripId,
                       //    ChildIds = scan.ChildScans.Select(ts => ts.ChildId).ToArray()
                       //}
                       // ).ToList()
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

           
           /* Trip.ChildIds.ToList().ForEach(id =>
            {
                if (_appDbContext.Children.Find (id) == null)
                {
                    throw new KeyNotFoundException("Could not find Child with id: " + id);
                }

                var tt = new TripChild
                {
                    ChildId = id,
                    Trip = Trip,
                };
                
                _appDbContext.TripChildren.Add(tt);
            });*/

           

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
