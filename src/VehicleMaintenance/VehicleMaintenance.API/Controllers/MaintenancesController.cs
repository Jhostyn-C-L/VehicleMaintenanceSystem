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
        public ActionResult<IEnumerable<MaintenanceDto>> GetAll()
        {
            var maintenances = _maintenances.Select(m => new MaintenanceDto
            {
                Id = m.Id,
                Description = m.Description,
                VehicleId = m.VehicleId,
                IsActive = m.IsActive
            }).ToList();
            return Ok(maintenances);
        }

        [HttpGet]
        [Route("ordered")]
        public ActionResult<IEnumerable<Maintenance>> GetAllOrdered()
        {
            var maintenances = _maintenances.OrderBy(m => m.Description).ToList();
            return Ok(maintenances);
        }

        [HttpGet("{id}")] 
        public ActionResult<MaintenanceDto> GetById(int id)
        {
            var maintenance = _maintenances.FirstOrDefault(m => m.Id == id);
            if (maintenance == null)
                return NotFound();
            var response = new MaintenanceDto
            {
                Id = maintenance.Id,
                Description = maintenance.Description,
                VehicleId = maintenance.VehicleId,
                IsActive = maintenance.IsActive
            };

            return Ok(response);
        }

        [HttpPost] 
        public ActionResult<Maintenance> Create(CreateMaintenanceDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return BadRequest("Description of maintenance is required...");
            }
            if (request.VehicleId <= 0)
            {
                return BadRequest("VehicleId must be provided and positive.");
            }
            var maintenance = new Maintenance
            {
                Description = request.Description,
                VehicleId = request.VehicleId,
                IsActive = true
            };


            int newId = _maintenances.Any() ? _maintenances.Max(m => m.Id) + 1 : 1;
            maintenance.Id = newId;
            maintenance.IsActive = true; 
            _maintenances.Add(maintenance);
            return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, request);
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
