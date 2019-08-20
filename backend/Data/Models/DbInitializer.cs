using backend.Data.Models;
using System.Collections.Generic;
using System.Linq;


namespace backend.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {

            context.Database.EnsureCreated();

            if (!context.Supervisors.Any())
            {
                context.Supervisors.AddRange(Supervisors.Select(c => c.Value));
            }

            if (!context.Children.Any())
            {
                context.AddRange(
                    new Child { Name = "George", LastName = "De Perre", Class = "1Handel" },
                    new Child { Name = "Laila", LastName = "Verachteren", Class = "3TWI" },
                    new Child { Name = "Frederic", LastName = "Dewale", Class = "2STW" });
            }
        }


        private static Dictionary<string, Supervisor> _supervisors;
        public static Dictionary<string, Supervisor> Supervisors
        {
            get
            {
                if (_supervisors == null)
                {
                    var supervisorList = new[]
                    {
                        new Supervisor { Name = "Frederic"},
                        new Supervisor {Name = "AnneMarie"},
                        new Supervisor {Name ="Dillan"}
                    };

                    _supervisors = new Dictionary<string, Supervisor>();

                    foreach(Supervisor supervisor in supervisorList)
                    {
                        _supervisors.Add(supervisor.Name, supervisor);
                    }
                }
                return _supervisors;
        }
        }
    }
}
