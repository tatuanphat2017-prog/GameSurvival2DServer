using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GameSurvival2DServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestConnectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestConnectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult TestDatabaseConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT 1", conn))
                    {
                        int result = (int)cmd.ExecuteScalar();

                        return Ok(new
                        {
                            message = "Kết nối SQL Server thành công",
                            sql_result = result
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Kết nối SQL Server thất bại",
                    error = ex.Message
                });
            }
        }
    }
}
