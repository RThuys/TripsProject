using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Repositories.Interfaces
{
    public interface ISupervisorRepository
    {
        Task<IEnumerable<Supervisor>> GetAllSupervisor();
        Task<Supervisor> GetSupervisorById(int supervisorId);
        Task<Supervisor> AddSupervisor(Supervisor Supervisor);
        Task RemoveSupervisor(int id);
    }
}
