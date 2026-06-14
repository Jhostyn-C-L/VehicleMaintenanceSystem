using Microsoft.EntityFrameworkCore;
using VehicleMaintenance.API.Models.Entities;

namespace VehicleMaintenance.API.Data
{
    public class VehicleMaintenanceDBContext : DbContext
    {
        public VehicleMaintenanceDBContext(
            DbContextOptions<VehicleMaintenanceDBContext> options): base( options ) 
        { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
    }
}
