using System.Collections.Generic;
using BeComfy.Common.Types.Enums;
using BeComfy.Services.Airplanes.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.EF
{
    public class AirplanesContext : DbContext
    {
        public DbSet<Airplane> Airplanes { get; set; }

        public AirplanesContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>()
                .Property(x => x.AvailableSeats)
                .HasConversion(
                    @in => JsonConvert.SerializeObject(@in), 
                    @out => JsonConvert.DeserializeObject<IDictionary<SeatClass, int>>(@out));
        }
    }
}