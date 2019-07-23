using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp.Model
{
    class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Supervisor { get; set; }
        public List<Child> Children { get; set; }
        public List<int> ChilId { get; set; }
        public DateTimeOffset Date { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
