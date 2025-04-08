using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T1_PR2_API.Context;
using T1_PR2_API.DTOs;
using T1_PR2_API.Model;

namespace T1_PR2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavGamesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        [HttpGet]
        public async Task<ActionResult<List<FavGame>>> GetFavGames(User user)
        {
            var favGames = await _context.FavGames.Select(g => new FavGameDTO
            {
                UserId = g.UserId,
                GameId = g.GameId
            }).ToListAsync();
            List<FavGame> favGamesList = new List<FavGame>();
            foreach (var favGame in favGames)
            {
                if (favGame.UserId == user.MyId)
                {
                    favGamesList.Add(new FavGame
                    {
                        UserId = favGame.UserId,
                        GameId = favGame.GameId
                    });
                }
            }
            return Ok(favGamesList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FavGameDTO>> GetFavGame(int id)
        {
            var favGame = await _context.FavGames.Where(g => g.Id == id).Select(g => new FavGameDTO
            {
                UserId = g.UserId,
                GameId = g.GameId
            }).FirstOrDefaultAsync();
            if (favGame == null)
            {
                return NotFound();
            }
            return Ok(favGame);
        }
        [HttpPost]
        public async Task<ActionResult<FavGameDTO>> PostFavGame(FavGameDTO favGameDTO)
        {
            var favGame = new FavGame
            {
                UserId = favGameDTO.UserId,
                GameId = favGameDTO.GameId
            };
            _context.FavGames.Add(favGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFavGame", new { id = favGame.Id }, favGameDTO);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFavGame(int id, FavGameDTO favGameDTO)
        {
            var favGame = await _context.FavGames.FindAsync(id);
            if (favGame == null)
            {
                return NotFound();
            }
            favGame.UserId = favGameDTO.UserId;
            favGame.GameId = favGameDTO.GameId;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFavGame(int id)
        {
            var favGame = await _context.FavGames.FindAsync(id);
            if (favGame == null)
            {
                return NotFound();
            }
            _context.FavGames.Remove(favGame);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
