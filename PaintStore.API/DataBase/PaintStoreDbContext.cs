using Microsoft.EntityFrameworkCore;
using PaintStore.Model;
using PaintStore.Model.Models;

namespace PaintStore.API.Database
{
    public class PaintStoreDbContext : DbContext
    {
        public PaintStoreDbContext(DbContextOptions<PaintStoreDbContext> options) : base(options)
        {
        }

        public DbSet<PaintProduct> PaintProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Week 4 作业范围只涉及 User / PaintProduct / Order，
            // Brand、PaintSpecification、Payment 暂不建表，避免它们影响这三张表的结构
            modelBuilder.Ignore<Brand>();
            modelBuilder.Ignore<PaintSpecification>();
            modelBuilder.Ignore<Payment>();

            modelBuilder.Entity<PaintProduct>().Ignore(p => p.Brand);
            modelBuilder.Entity<PaintProduct>().Ignore(p => p.Specification);
            modelBuilder.Entity<User>().Ignore(u => u.HistoricalPayment);

            base.OnModelCreating(modelBuilder);
        }
    }
}
