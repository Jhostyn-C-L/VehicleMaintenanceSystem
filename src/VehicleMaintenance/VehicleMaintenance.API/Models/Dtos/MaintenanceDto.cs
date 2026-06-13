namespace VehicleMaintenance.API.Models.Entities
{
    public class MaintenanceDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int VehicleId { get; set; }
        public bool IsActive { get; set; }
    }
}
