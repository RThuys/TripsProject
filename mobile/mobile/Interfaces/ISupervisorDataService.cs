using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Interfaces
{
    public interface ISupervisorDataService
    {
        Task<string> AddSupervisor(string supervisor);
        Task<Supervisor> GetSupervisor();
        Task<Supervisor> AddSupervisor();

        //DEMO purpose
        Task<Supervisor> DeleteSupervisor();

    }
}
