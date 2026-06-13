using Microsoft.AspNetCore.Mvc;
using VehicleMaintenance.API.Models.Entities;
using System.Linq;

namespace VehicleMaintenance.API.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenancesController: ControllerBase
    {
        private static readonly List<Maintenance> _maintenances = new List<Maintenance>
{
    new Maintenance { Id = 1, Description = "Cambio de aceite", VehicleId = 101, IsActive = true },
    new Maintenance { Id = 2, Description = "Revisión de frenos", VehicleId = 102, IsActive = true },
    new Maintenance { Id = 3, Description = "Mantenimiento general", VehicleId = 103, IsActive = false }
};
        [HttpGet] 
        public ActionResult<IEnumerable<Maintenance>> GetAll()
        {
            return Ok(_maintenances);
        }

        [HttpGet]
        [Route("ordered")]
        public ActionResult<IEnumerable<Maintenance>> GetAllOrdered()
        {
            var maintenances = _maintenances.OrderBy(m => m.Description).ToList();
            return Ok(maintenances);
        }

        [HttpGet("{id}")] 
        public ActionResult<Maintenance> GetById(int id)
        {
            var maintenance = _maintenances.FirstOrDefault(m => m.Id == id);
            if (maintenance == null)
                return NotFound();

            return Ok(maintenance);
        }

        [HttpPost] 
        public ActionResult<Maintenance> Create(Maintenance maintenance)
        {
            if (string.IsNullOrWhiteSpace(maintenance.Description))
            {
                return BadRequest("Description of maintenance is required...");
            }
            if (maintenance.VehicleId <= 0)
            {
                return BadRequest("VehicleId must be provided and positive.");
            }
            
            int newId = _maintenances.Any() ? _maintenances.Max(m => m.Id) + 1 : 1;
            maintenance.Id = newId;
            maintenance.IsActive = true; 
            _maintenances.Add(maintenance);
            return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, maintenance);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Maintenance maintenance)

        {
            var existing = _maintenances.FirstOrDefault(m => m.Id == id);
            if (existing == null)
                return NotFound();

            existing.Description = maintenance.Description;
            existing.VehicleId = maintenance.VehicleId;
            existing.IsActive = maintenance.IsActive;
            return NoContent();
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            var existing = _maintenances.FirstOrDefault(m => m.Id == id);
            if (existing == null)
                return NotFound();

            _maintenances.Remove(existing);
            return NoContent();
        }

    }
}
