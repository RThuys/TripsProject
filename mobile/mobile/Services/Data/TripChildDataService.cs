using Akavache;
using mobile.Constants;
using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services.Data
{
    public class TripChildDataService : ITripChildDataService
    {

        private readonly IGenericRepository _genericRepository;

        public TripChildDataService(IGenericRepository genericRepository) : base()
        {
            this._genericRepository = genericRepository;
        }

        public async Task<IEnumerable<TripChild>> GetAlTripsChildren()
        {
            var tripChild = await _genericRepository.GetAsync<List<TripChild>>(Api.BASE_URL + Api.TRIPS_CHILDREN_API);
            return tripChild;
        }

        public Task<TripChild> GetTripChild(int ChildId)
        {
            throw new NotImplementedException();
        }

    }
}
