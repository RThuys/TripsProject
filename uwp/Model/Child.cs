using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp.Model
{
    class Child
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Class { get; set; }
        public List<Scan> Scans { get; set; }
        public List<Trip> Trips { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
