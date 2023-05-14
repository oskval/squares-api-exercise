using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coordinate>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }

        public DbSet<Coordinate> Coordinates { get; set; }
    }
}
