using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp_app.Model
{
    public class TripChild
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int ChildId { get; set; }
        public bool Scanned { get; set; }

        public override string ToString()
        {
            return $"{TripId} {ChildId}";
        }
    }
}
