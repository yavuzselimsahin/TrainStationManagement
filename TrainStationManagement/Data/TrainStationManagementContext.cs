using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainStationManagement.Models;

namespace TrainStationManagement.Data
{
    public class TrainStationManagementContext : DbContext
    {
        public TrainStationManagementContext (DbContextOptions<TrainStationManagementContext> options)
            : base(options)
        {
        }

        public DbSet<TrainStationManagement.Models.Train> Train { get; set; } = default!;

        public DbSet<TrainStationManagement.Models.TrainStation> TrainStation { get; set; } = default!;

        public DbSet<TrainStationManagement.Models.User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>()
                .HasOne(t => t.DepartureStation)
                .WithMany(s => s.DepartureTrains)
                .HasForeignKey(t => t.DepartureStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Train>()
                .HasOne(t => t.ArrivalStation)
                .WithMany(s => s.ArrivalTrains)
                .HasForeignKey(t => t.ArrivalStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
