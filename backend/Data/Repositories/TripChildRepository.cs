using backend.Data.Models;
using backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Repositories
{
    public class TripChildRepository : ITripChildRepository
    {
        private readonly AppDbContext _context;

        public TripChildRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<TripChild> AddTripChild(TripChild TripChild)
        {
            var tripChild = _context.TripChildren.Add(TripChild);
            await _context.SaveChangesAsync();
            return tripChild.Entity;
        }

        public async Task<IEnumerable<TripChild>> GetAllTripsChildren()
        {
            return await _context.TripChildren.ToListAsync();
        }

        public async Task<TripChild> GetTripChildById(int Id)
        {
            return await _context.TripChildren.FirstOrDefaultAsync(trip => trip.Id == Id);
        }

        public async Task<IEnumerable<TripChild>> GetTripChildrenByTripId(int tripId)
        {
            string query = "SELECT * FROM TripChildren tc Where tc.TripId = {0}";
            return await _context.TripChildren.FromSql(query, tripId).ToListAsync();

        }

        public async Task<IEnumerable<Child>> GetTripChildrenByTripIdChildren(int tripId)
        {
            string query = "SELECT * FROM TripChildren tc Where tc.TripId = {0}";
            string queryChildren = "SELECT * from Children where id = {0}";
            List<TripChild> tripChildren  = await _context.TripChildren.FromSql(query, tripId).ToListAsync();
            List<Child> children = new List<Child>();
            foreach(TripChild child in tripChildren)
            {
                children.Add(_context.Children.FromSql(queryChildren, child.ChildId).FirstOrDefault());
            }
            return children;

        }

        public async Task<IEnumerable<Child>> GetTripChildrenByTripIdChildrenNot(int tripId)
        {
            string query = "SELECT * FROM TripChildren tc Where tc.TripId = {0}";
            string queryChildren = "SELECT * from Children where id = {0}";
            List<TripChild> tripChildren = await _context.TripChildren.FromSql(query, tripId).ToListAsync();
            List<Child> children = new List<Child>();
            List<Child> AllChildren = await _context.Children.ToListAsync();
            foreach (TripChild child in tripChildren)
            {
                children.Add(_context.Children.FromSql(queryChildren, child.ChildId).FirstOrDefault());
            }

            List<Child> childrenNotInTrip = AllChildren.Except(children).ToList();

            return childrenNotInTrip;

        }

        public Task RemoveTripChild(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TripChild> UpdateTripChild(TripChild TripChild)
        {
            throw new NotImplementedException();
        }
    }
}
