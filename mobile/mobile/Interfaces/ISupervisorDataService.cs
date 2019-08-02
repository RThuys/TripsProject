using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Interfaces
{
    public interface ISupervisorDataService
    {
        Task AddSupervisor(Supervisor supervisor);
        Task<Supervisor> GetSupervisor();

        //DEMO purpose
        Task<Supervisor> DeleteSupervisor();

    }
}
