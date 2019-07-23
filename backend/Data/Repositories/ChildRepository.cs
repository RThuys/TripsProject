using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backend.Data.Models;
using backend.Data.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class ChildRepository : IChildRepository
    {
        private readonly AppDbContext _context;

        public ChildRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<Child> AddChild(Child Child)
        {
            var child = _context.Children.Add(Child);
            await _context.SaveChangesAsync();
            return child.Entity;
        }

        public async Task<Child> UpdateChild(Child Child)
        {
            var child = _context.Children.Update(Child);
            await _context.SaveChangesAsync();
            return child.Entity;
        }


        public async Task<IEnumerable<Child>> GetAllChildren()
        {
            return await _context.Children.ToListAsync();
        }

        public async Task<Child> GetChildById(int Id)
        {
            return await _context.Children.FirstOrDefaultAsync(child => child.Id == Id);
        }

        public async Task RemoveChild (int Id)
        {
            var Child = _context.Children.Find(Id);
            if (Child == null)
            {
                throw new KeyNotFoundException("Could not find Child with id: " + Id);
            }

            _context.Children.Remove(Child);
            await _context.SaveChangesAsync();
        }
    }
}
