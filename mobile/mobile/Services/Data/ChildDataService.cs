using Akavache;
using mobile.Constants;
using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services.Data
{
    public class ChildDataService : BaseService, IChildDataService
    {
        private readonly IGenericRepository _genericRepository;
        

        public ChildDataService(IGenericRepository genericRepository, IBlobCache cache = null): base(cache)
        {
            this._genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Child>> GetAllChildren()
        { 
            var children = await _genericRepository.GetAsync<List<Child>>(Api.BASE_URL + Api.CHILDREN_API);
            return children;
        }

        public Task<Child> GetChildById(int ChildId)
        {
            throw new NotImplementedException();
        }
    }
}
