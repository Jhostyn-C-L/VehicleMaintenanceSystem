using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using VehicleMaintenance.API.Models.Entities;
using VehicleMaintenance.API.Data;

namespace VehicleMaintenance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class VehiclesController: ControllerBase
    {/*
        private static readonly List<Vehicle> _vehicles = new List<Vehicle>
{
    new Vehicle { Id = 1, LicensePlate = "A123BC", Brand = "Toyota", IsActive = true },
    new Vehicle { Id = 2, LicensePlate = "D456EF", Brand = "Honda", IsActive = true },
    new Vehicle { Id = 3, LicensePlate = "G789HI", Brand = "Ford", IsActive = true }
};*/
        private readonly VehicleMaintenanceDBContext _context;
        public VehiclesController(
            VehicleMaintenanceDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAll()
        {
            return Ok(_context.Vehicles.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetById(int id)
        {
            //var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
[HttpPost] 
        public ActionResult<Vehicle> Create(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
            
            if (string.IsNullOrWhiteSpace(vehicle.LicensePlate))
            {
                return BadRequest("LicensePlate of vehicle is required...");
            }
            return CreatedAtAction(
nameof(GetById),
new { id = vehicle.Id },
vehicle);}
        /*
        int newId = _vehicles.Any() ? _vehicles.Max(m => m.Id) + 1 : 1;
        vehicle.Id = newId;
        if (vehicle.IsActive == false)
        {
            vehicle.IsActive = true;
        }       
_vehicles.Add(vehicle);*/
        //_context.Vehicles.Add(vehicle);
        //_context.SaveChanges();

[HttpPut("{id}")] 
public IActionResult Update(int id, Vehicle vehicle)
{
            //var existing = _vehicles.FirstOrDefault(m => m.Id == id);
    var existing = _context.Vehicles.FirstOrDefault(v => v.Id == id);

            if (existing == null)
    {
        return NotFound();
    }
    
    existing.LicensePlate = vehicle.LicensePlate;
    existing.Brand = vehicle.Brand;
    existing.IsActive = vehicle.IsActive;
            _context.SaveChanges();
return NoContent();
}

[HttpDelete("{id}")] 
public IActionResult Delete(int id)
{
    var existing = _context.Vehicles.FirstOrDefault(v => v.Id == id);
    if (existing == null)
    {
        return NotFound();
    }
    _context.Vehicles.Remove(existing);
    _context.SaveChanges();
    return NoContent();
        }
    }
}
