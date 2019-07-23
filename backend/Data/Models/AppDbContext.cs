using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace backend.Data.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Child> Children { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripChild> TripChildren { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
