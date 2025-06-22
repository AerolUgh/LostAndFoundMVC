using LostAndFoundMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LostAndFoundMVC.Data
{
    public class LostAndFoundContext : DbContext
    {
        public LostAndFoundContext(DbContextOptions<LostAndFoundContext> options)
            : base(options)
        {
        }
        public DbSet<LostItems> LostItems { get; set; } = null!;
        public DbSet<FoundItems> FoundItems { get; set; } = null!;
        public DbSet<Reports> Reports { get; set; } = null!;
        public DbSet<Claimed> Claimed { get; set; } = null!;
        public DbSet<NotClaimed> NotClaimed { get; set; } = null!;
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    // Additional model configuration can go here if needed
        //}
    }
}
