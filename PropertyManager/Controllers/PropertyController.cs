using Microsoft.AspNetCore.Mvc;
using PropertyManager.Repositories;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(_propertyRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            var property = _propertyRepository.GetById(id);
            if (property == null) 
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpPost]
        public IActionResult Post(Property property)
        {
            _propertyRepository.Add(property);
            return CreatedAtAction("Get", new { id = property.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Property property)
        {
            if (id != property.Id)
            {
                return BadRequest();
            }

            _propertyRepository.Update(property);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _propertyRepository.Delete(id);
            return NoContent();
        }

    }
}
