using Microsoft.AspNetCore.Mvc;
using PropertyManager.Repositories;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var tenants = _tenantRepository.GetAll();
            return Ok(tenants);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tenant = _tenantRepository.GetById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        [HttpPost]
        public IActionResult Post(Tenant tenant)
        {
            _tenantRepository.Add(tenant);
            return CreatedAtAction("Get", new { id = tenant.Id }, tenant);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tenantRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return BadRequest();
            }

            _tenantRepository.Update(tenant);
            return NoContent();
        }
    }
}
