using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;
using NZWalksAPI.Utils;

namespace NZWalksAPI.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController(IRegionRepository repository) : ControllerBase
    {
        private readonly IRegionRepository repository = repository;

        // GET: api/regions
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            var regions = await repository.GetRegions();
            if (regions == null)
            {
                return NotFound("Regions not found");
            }

            return Ok(regions);
        }

        // GET: api/regions/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<Region>> GetRegion(Guid id)
        {
            var region = await repository.GetRegion(id);

            if (region == null)
            {
                return NotFound("Region not found");
            }

            return Ok(region);
        }

        // PUT: api/regions/{id}
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRegion(Guid id, UpdateRegionDto regionDto)
        {
            var region = await repository.PutRegion(id, regionDto);
            if (region.errorMessage != null)
            {
                return BadRequest(new { Error = region.errorMessage });
            }

            return Ok(region);
        }

        // POST: api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Region>> PostRegion(CreateRegionDto regionDto)
        {
            var (region, errorMessage) = await repository.PostRegion(regionDto);
            if (errorMessage != null)
            {
                return BadRequest(new { Error = errorMessage });
            }

            return Ok(region);
        }

        // DELETE: api/regions/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
