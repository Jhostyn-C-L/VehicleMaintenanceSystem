namespace VehicleMaintenance.API.Models.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }

        public string Brand { get; set; }

        public bool IsActive { get; set; }
        public List<Maintenance> Maintenances { get; set; } = new();
    }
}
