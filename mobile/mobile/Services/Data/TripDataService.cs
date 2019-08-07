using mobile.Constants;
using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services.Data
{
    class TripDataService : ITripDataService
    {
        private readonly IGenericRepository _genericRepository;


        public TripDataService(IGenericRepository genericRepository) : base()
        {
            this._genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Trip>> GetAllTrips()
        {
            var trips = await _genericRepository.GetAsync<List<Trip>>(Api.BASE_URL + Api.TRIPS_API);
            return trips;
        }

        public Task<Trip> GetTripById(int TripId)
        {
            throw new NotImplementedException();
        }
    }
}
