using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class Supervisor
    {
        [PrimaryKey, AutoIncrement]
        public int SupervisorID { get; set; }
        public string Name { get; set; }
    }
}
