using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace WebAPI.DataAccess
{
    public class KinderGardenContext : DbContext {
        private static string PATH = "C:/Dev/dotnet/DNPEksamen-304829/WebAPI";
        
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Child> Children { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {PATH}/Toys.db");
        }
    }
}