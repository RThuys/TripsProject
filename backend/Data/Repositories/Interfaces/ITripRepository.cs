using System.Collections.Generic;
using System.Threading.Tasks;

using backend.Data.Models;
using backend.Models;

namespace backend.Data.Repositories
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAllTrips();
        Task<Trip> GetTripById(int tripId);
        Task<Trip> AddTrip(Trip Trip);
        Task RemoveTrip(int id);
    }
}
