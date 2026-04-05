using Microsoft.EntityFrameworkCore;
using EquipmentAPI.Models;

namespace EquipmentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Equipment> Equipments => Set<Equipment>();
    }
}

