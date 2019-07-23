using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp.Model
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SupervisorId { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
