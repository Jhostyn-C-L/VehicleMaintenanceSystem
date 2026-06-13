using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using VehicleMaintenance.API.Models.Entities;

namespace VehicleMaintenance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class VehiclesController: ControllerBase
    {
        private static readonly List<Vehicle> _vehicles = new List<Vehicle>
{
    new Vehicle { Id = 1, LicensePlate = "A123BC", Brand = "Toyota", IsActive = true },
    new Vehicle { Id = 2, LicensePlate = "D456EF", Brand = "Honda", IsActive = true },
    new Vehicle { Id = 3, LicensePlate = "G789HI", Brand = "Ford", IsActive = true }
};
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAll()
        {
            return Ok(_vehicles);
        }

        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetById(int id)
        {
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
[HttpPost] 
        public ActionResult<Vehicle> Create(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.LicensePlate))
            {
                return BadRequest("LicensePlate of vehicle is required...");
            }
            int newId = _vehicles.Any() ? _vehicles.Max(m => m.Id) + 1 : 1;
            vehicle.Id = newId;
            if (vehicle.IsActive == false)
            {
                vehicle.IsActive = true;
            }       
    _vehicles.Add(vehicle);

return CreatedAtAction(
nameof(GetById),
new { id = vehicle.Id }, 
vehicle 
);
}
[HttpPut("{id}")] 
public IActionResult Update(int id, Vehicle vehicle)
{
    var existing = _vehicles.FirstOrDefault(m => m.Id == id);
    if (existing == null)
    {
        return NotFound();
    }
    
    existing.LicensePlate = vehicle.LicensePlate;
    existing.Brand = vehicle.Brand;
    existing.IsActive = vehicle.IsActive;
    
return NoContent();
}

[HttpDelete("{id}")] 
public IActionResult Delete(int id)
{
    var existing = _vehicles.FirstOrDefault(m => m.Id == id);
    if (existing == null)
    {
        return NotFound();
    }
    _vehicles.Remove(existing);
    return NoContent();
        }
    }
}
