using mobile.Interfaces;
using mobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile.Services.Data
{
    public class SupervisorDataService
    {
        readonly SQLiteAsyncConnection _database;

        public SupervisorDataService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Supervisor>().Wait();
        }

        public Task<List<Supervisor>> GetSupervisorsAsync()
        {
            return _database.Table<Supervisor>().ToListAsync();
        }

        public Task<Supervisor> GetSupervisorAsync(int id)
        {
            return _database.Table<Supervisor>()
                .Where(i => i.SupervisorID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveSupervisorAsync(Supervisor supervisor)
        {
            if (supervisor.SupervisorID != 0)
            {
                return _database.UpdateAsync(supervisor);
            } else
            {
                return _database.InsertAsync(supervisor);
            }
        }

        public Task<int> DeletenoteAsync(Supervisor supervisor)
        {
            return _database.DeleteAsync(supervisor);
        }
    }
}
