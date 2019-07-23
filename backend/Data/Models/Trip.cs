using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace backend.Data.Models
{
    public partial class Trip
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Title { get; set; }
        public int SupervisorId { get; set; }
        public DateTime Date { get; set; }
        
    }
}
