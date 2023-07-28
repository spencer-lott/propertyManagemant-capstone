using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Repositories;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceHistoryController : ControllerBase
    {
        private readonly IMaintenanceHistoryRepository _maintenanceHistoryRepository;

        public MaintenanceHistoryController(IMaintenanceHistoryRepository maintenanceHistoryRepository)
        {
            _maintenanceHistoryRepository = maintenanceHistoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var notes = _maintenanceHistoryRepository.GetAll();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var note = _maintenanceHistoryRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public IActionResult Post(MaintenanceHistory note)
        {
            _maintenanceHistoryRepository.Add(note);
            return CreatedAtAction("Get", new { id = note.Id }, note);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _maintenanceHistoryRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, MaintenanceHistory note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _maintenanceHistoryRepository.Update(note);
            return NoContent();
        }


    }
}
