using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Repositories.Interfaces
{
    public interface ITripChildRepository
    {
        Task<IEnumerable<TripChild>> GetAllTripsChildren();
        Task<TripChild> GetTripChildById(int Id);
        Task<IEnumerable<TripChild>> GetTripChildByTripId(int tripId);
        Task<TripChild> AddTripChild(TripChild TripChild);
        Task<TripChild> UpdateTripChild(TripChild TripChild);
        Task RemoveTripChild(int Id);
    }
}
