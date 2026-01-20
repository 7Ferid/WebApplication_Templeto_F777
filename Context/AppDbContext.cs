using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplication_Templeto_F777.Models;

namespace WebApplication_Templeto_F777.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Profession> Professions { get; set; }
    }
}
