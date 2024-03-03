using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;
using NZWalksAPI.Utils;

namespace NZWalksAPI.Controllers
{
    [Route("api/difficulties")]
    [ApiController]
    public class DifficultiesController(IDifficultyRepository repository) : ControllerBase
    {
        private readonly IDifficultyRepository repository = repository;

        // GET: api/difficulties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Difficulty>>> GetDifficulties()
        {
            throw new Exception("Test error");
            var difficulties = await repository.GetDifficulties();
            if (difficulties == null)
            {
                return NotFound();
            }

            return Ok(difficulties);
        }

        // GET: api/difficulties/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Difficulty>> GetDifficulty(Guid id)
        {
            var difficulty = await repository.GetDifficulty(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return Ok(difficulty);
        }

        // PUT: api/difficulties/{id}
        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> PutDifficulty(Guid id, DifficultyDto difficultyDto)
        {
            var difficulty = await repository.PutDifficulty(id, difficultyDto);
            if (difficulty == null)
            {
                return BadRequest();
            }

            return Ok(difficulty);
        }

        // POST: api/difficulties
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Difficulty>> PostDifficulty(DifficultyDto difficultyDto)
        {
            var difficulty = await repository.PostDifficulty(difficultyDto);

            if (difficulty == null)
            {
                return BadRequest();
            }

            return Ok(difficulty);
        }

        // DELETE: api/difficulties/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDifficulty(Guid id)
        {
            var difficulty = await repository.DeleteDifficulty(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
