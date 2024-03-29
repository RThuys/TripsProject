﻿using backend.Data.Models;
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

        public Task<TripChild> GetTripChildById(int Id)
        {
            throw new NotImplementedException();
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
