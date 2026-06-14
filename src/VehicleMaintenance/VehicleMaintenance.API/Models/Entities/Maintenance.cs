namespace VehicleMaintenance.API.Models.Entities
{
    public class Maintenance
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }
        public bool IsActive { get; set; }
    }
}
