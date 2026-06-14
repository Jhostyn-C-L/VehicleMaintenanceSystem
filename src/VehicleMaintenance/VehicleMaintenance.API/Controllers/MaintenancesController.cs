using Microsoft.AspNetCore.Mvc;
using VehicleMaintenance.API.Models.Entities;
using VehicleMaintenance.API.Data;
using System.Linq;

namespace VehicleMaintenance.API.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenancesController: ControllerBase
    {
        private readonly VehicleMaintenanceDBContext _context;
        public MaintenancesController(VehicleMaintenanceDBContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<IEnumerable<MaintenanceDto>> GetAll()
        {
            var maintenances = _context.Maintenances.Select(m => new MaintenanceDto
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
        
        public ActionResult<IEnumerable<MaintenanceDto>> GetAllOrdered()
        {
            var maintenances = _context.Maintenances.OrderBy(m => m.Description).Select(m => new MaintenanceDto
            {
                Id = m.Id,
                Description = m.Description,
                VehicleId = m.VehicleId,
                IsActive = m.IsActive
            }).ToList();
            return Ok(maintenances);
        }
        [HttpGet("{id}")] 
        public ActionResult<MaintenanceDto> GetById(int id)
        {
            var maintenance = _context.Maintenances.FirstOrDefault(m => m.Id == id);
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
            var vehicleExists = _context.Vehicles.Any(v => v.Id == request.VehicleId);
            if (!vehicleExists)
            {
                return BadRequest("The specified vehicle does not exist.");
            }
            var maintenance = new Maintenance
            {
                Description = request.Description,
                VehicleId = request.VehicleId,
                IsActive = true
            };
            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
            return CreatedAtAction(
                nameof(GetById),
                new { id = maintenance.Id }, maintenance); }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Maintenance maintenance)

        {
            var existing = _context.Maintenances.FirstOrDefault(m => m.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Description = maintenance.Description;
            existing.VehicleId = maintenance.VehicleId;
            existing.IsActive = maintenance.IsActive;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            var existing = _context.Maintenances.FirstOrDefault(m => m.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            _context.Maintenances.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
