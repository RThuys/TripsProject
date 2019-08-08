using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Interfaces
{
    public interface ITripChildDataService
    {
        Task<IEnumerable<TripChild>> GetAlTripsChildren();
        Task<TripChild> GetTripChild(int ChildId);
    }
}
