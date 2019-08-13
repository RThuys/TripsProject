using Akavache;
using mobile.Constants;
using mobile.Interfaces;
using mobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services.Data
{
    public class SupervisorApiDataService: BaseService,  ISupervisorDataService
    {

        private readonly IGenericRepository _genericRepository;

        public SupervisorApiDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            this._genericRepository = genericRepository;
        }

        public Task<string> AddSupervisor(String supervisor)
        {
            return _genericRepository.PostAsync(Api.BASE_URL + Api.SUPERVISORS, supervisor);
        }

        public Task<Supervisor> AddSupervisor()
        {
            throw new NotImplementedException();
        }

        public Task<Supervisor> DeleteSupervisor()
        {
            throw new NotImplementedException();
        }

        public Task<Supervisor> GetSupervisor()
        {
            throw new NotImplementedException();
        }
    }
}
