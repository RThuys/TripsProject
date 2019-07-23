using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backend.Data.Models;

namespace backend.Data.Repositories.Interfaces
{
    public interface IChildRepository
    {
        Task<IEnumerable<Child>> GetAllChildren();
        Task<Child> GetChildById(int Id);
        Task<Child> AddChild(Child Child);
        Task<Child> UpdateChild(Child Child);
        Task RemoveChild(int Id);
    }
}
