using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;
using NZWalksAPI.Utils;

namespace NZWalksAPI.Controllers
{
    [Route("api/walks")]
    [ApiController]
    public class WalksController(IWalkRepository repository) : ControllerBase
    {
        private readonly IWalkRepository repository = repository;

        // GET: api/walks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk>>> GetWalks()
        {
            var walks = await repository.GetWalks();
            if (walks == null)
            {
                return NotFound();
            }

            return Ok(walks);
        }

        // GET: api/walks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Walk>> GetWalk(Guid id)
        {
            var walk = await repository.GetWalk(id);

            if (walk == null)
            {
                return NotFound();
            }

            return walk;
        }

        // PUT: api/walks/{id}
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> PutWalk(Guid id, CreateWalkDto walkDto)
        {
            var walk = await repository.PutWalk(id, walkDto);
            if (walk == null)
            {
                return BadRequest();
            }

            return Ok(walk);
        }

        // POST: api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseWalkDto>> PostWalk(CreateWalkDto createWalkDto)
        {
            var walk = await repository.PostWalk(createWalkDto);
            if (walk == null)
            {
                return BadRequest();
            }

            return walk;
        }

        // DELETE: api/walks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walk = await repository.DeleteWalk(id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
