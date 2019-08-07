using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SupervisorId { get; set; }
        public DateTime Date { get; set; }
    }
}
