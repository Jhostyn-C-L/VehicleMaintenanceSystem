namespace VehicleMaintenance.API.Models.Entities
{
    public class CreateMaintenanceDto
    {
        public string Description { get; set; } = null!;
        public int VehicleId { get; set; }
    }
}
