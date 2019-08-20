using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            modelBuilder.Entity<Supervisor>(ConfigureSupervisor);
            modelBuilder.Entity<Child>(ConfigureChild);
            modelBuilder.Entity<Trip>(ConfifureTrip);
            modelBuilder.Entity<TripChild>(configureTripChild);
        }

        private void configureTripChild(EntityTypeBuilder<TripChild> obj)
        {
        }

        private void ConfifureTrip(EntityTypeBuilder<Trip> obj)
        {
        }

        private void ConfigureChild(EntityTypeBuilder<Child> obj)
        {
        }

        private void ConfigureSupervisor(EntityTypeBuilder<Supervisor> obj)
        {
            
        }
    }
}
