using Microsoft.AspNetCore.Mvc;
using GameSurvival2DServer.Database;
using GameSurvival2DServer.MODELL;

namespace GameSurvival2DServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly GameDbContext _context;

        public ProfileController(GameDbContext context)
        {
            _context = context;
        }

        [HttpPut("update")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            if (request == null || request.AccountId <= 0)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var profile = _context.PlayerProfiles.FirstOrDefault(p => p.AccountId == request.AccountId);

            if (profile == null)
            {
                profile = new PlayerProfile
                {
                    AccountId = request.AccountId,
                    DisplayName = request.DisplayName,
                    Level = request.Level,
                    TotalPlayTime = request.TotalPlayTime
                };

                _context.PlayerProfiles.Add(profile);
            }
            else
            {
                profile.DisplayName = request.DisplayName;
                profile.Level = request.Level;
                profile.TotalPlayTime = request.TotalPlayTime;
            }

            _context.SaveChanges();

            return Ok(new
            {
                message = "Cập nhật thông tin thành công",
                profile.AccountId,
                profile.DisplayName,
                profile.Level,
                profile.TotalPlayTime
            });
        }
    }
}