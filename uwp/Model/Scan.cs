using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp.Model
{
    class Scan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TripId { get; set; }
        public List<Child> Children { get; set; }

        public override string ToString()
        {
            return $"{Name} got scanned for trip {TripId}";
        }
    }
}
