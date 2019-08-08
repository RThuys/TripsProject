using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class TripChild
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int ChildId { get; set; }
        public string Name { get; set; }
        public bool Scanned { get; set; }
    }
}
