using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Interfaces
{
    public interface IChildDataService
    {
        Task<IEnumerable<Child>> GetAllChildren();
        Task<Child> GetChildById(int ChildId);
    }
}
