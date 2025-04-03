using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T1_PR2_API.DTOs;
using T1_PR2_API.Model;
using T1_PR2_API.Context;

namespace T1_PR2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _context.Games.Select(g => new GameDTO
            {
                Name = g.Name,
                DevTeam = g.DevTeam
            }).ToListAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            var game = await _context.Games.Where(g => g.Id == id).Select(g => new GameDTO
            {
                Name = g.Name,
                DevTeam = g.DevTeam
            }).FirstOrDefaultAsync();
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameDTO>> PostGame(GameDTO gameDTO)
        {
            var game = new Game
            {
                Name = gameDTO.Name,
                DevTeam = gameDTO.DevTeam
            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGame", new { id = game.Id }, gameDTO);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateGame(int id, GameDTO gameDTO)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            game.Name = gameDTO.Name;
            game.DevTeam = gameDTO.DevTeam;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
