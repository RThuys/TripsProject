using backend.Data.Models;
using backend.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Repositories
{
    public class SupervisorRepository: ISupervisorRepository
    {
        private readonly AppDbContext _context;

        public SupervisorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Supervisor> AddSupervisor(Supervisor Supervisor)
        {
            var supervisor = _context.Supervisors.Add(Supervisor);
            await _context.SaveChangesAsync();
            return supervisor.Entity;
        }

        public async Task<IEnumerable<Supervisor>> GetAllSupervisor()
        {
            return await _context.Supervisors.ToListAsync();
        }

        public async Task<IEnumerable<Supervisor>> GetAllSupervisors()
        {
            return await _context.Supervisors.ToListAsync();
        }

        public async Task<Supervisor> GetSupervisorById(int supervisorId)
        {
            return await _context.Supervisors.FirstOrDefaultAsync(supervisor => supervisor.Id == supervisorId);
        }

        public Task RemoveSupervisor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
