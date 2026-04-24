using Microsoft.AspNetCore.Mvc;
using GameSurvival2DServer.Database;
using GameSurvival2DServer.MODELL;

namespace GameSurvival2DServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly GameDbContext _context;

        public LeaderboardController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLeaderboard()
        {
            var data = (from lb in _context.Leaderboards
                        join account in _context.Accounts on lb.AccountId equals account.Id
                        join profile in _context.PlayerProfiles on account.Id equals profile.AccountId into pg
                        from profile in pg.DefaultIfEmpty()
                        orderby lb.Score descending, lb.SurvivalTime descending
                        select new
                        {
                            DisplayName = profile != null ? profile.DisplayName : account.Username,
                            lb.Score,
                            lb.SurvivalTime
                        })
                        .Take(10)
                        .ToList();

            var result = data.Select((x, index) => new LeaderboardResponse
            {
                Rank = index + 1,
                DisplayName = x.DisplayName,
                Score = x.Score,
                SurvivalTime = x.SurvivalTime
            }).ToList();

            return Ok(result);
        }
    }
}