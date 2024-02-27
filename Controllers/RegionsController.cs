using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController(IRegionRepository repository) : ControllerBase
    {
        private readonly IRegionRepository repository = repository;

        // GET: api/regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            var regions = await repository.GetRegions();
            if (regions == null)
            {
                return NotFound();
            }

            return Ok(regions);
        }

        // GET: api/regions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(Guid id)
        {
            var region = await repository.GetRegion(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        // PUT: api/regions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion(Guid id, RegionDto regionDto)
        {
            var region = await repository.PutRegion(id, regionDto);
            if (region == null)
            {
                return BadRequest();
            }

            return Ok(region);
        }

        // POST: api/regions
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion(RegionDto regionDto)
        {
            var region = await repository.PostRegion(regionDto);
            if (region == null)
            {
                return BadRequest();
            }

            return Ok(region);
        }

        // DELETE: api/regions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await repository.DeleteRegion(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
