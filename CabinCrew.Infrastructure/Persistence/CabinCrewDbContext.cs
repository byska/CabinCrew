using CabinCrew.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabinCrew.Infrastructure.Persistence
{
    public class CabinCrewDbContext : DbContext
    {
        public DbSet<CabinAttendant> CabinAttendants { get; set; }

        public CabinCrewDbContext(DbContextOptions<CabinCrewDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CabinCrewDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
