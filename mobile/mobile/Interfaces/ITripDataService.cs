using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Interfaces
{
    public interface ITripDataService
    {
        Task<IEnumerable<Trip>> GetAllTrips();
        Task<Trip> GetTripById(int ChildId);
    }
}
