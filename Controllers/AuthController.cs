using Microsoft.AspNetCore.Mvc;
using GameSurvival2DServer.Database;
using GameSurvival2DServer.MODELL;
using Microsoft.EntityFrameworkCore; // Thêm cái này nếu muốn dùng FirstOrDefaultAsync

namespace GameSurvival2DServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly GameDbContext _context;

        public AuthController(GameDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Thiếu tên đăng nhập hoặc mật khẩu" });
            }

            var existingUser = _context.Accounts.FirstOrDefault(x => x.Username == request.Username);
            if (existingUser != null)
            {
                return Conflict(new { message = "Tên đăng nhập đã tồn tại" });
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var account = new Account
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword,
                IsBanned = false,
                Role = "User",
                CreatedAt = DateTime.Now
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Đăng ký thành công",
                account.Id,
                account.Username,
                account.Email
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1. Kiểm tra đầu vào trống
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Thiếu tên đăng nhập hoặc mật khẩu" });
            }

            // 2. Tìm tài khoản trong database
            var account = _context.Accounts.FirstOrDefault(x => x.Username == request.Username);

            if (account == null)
            {
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });
            }

            // 3. KIỂM TRA KHÓA TÀI KHOẢN (Đưa lên trước để báo lỗi sớm)
            if (account.IsBanned)
            {
                // Trả về 403 Forbidden để đúng với yêu cầu "Tài khoản bị khóa"
                return StatusCode(403, new { message = "Tài khoản đã bị khóa" });
            }

            // 4. Kiểm tra mật khẩu bằng BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash);

            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });
            }

            // 5. Trả về kết quả thành công
            return Ok(new
            {
                message = "Đăng nhập thành công",
                account.Id,
                account.Username,
                account.Email,
                account.Role
            });
        }
    }
}