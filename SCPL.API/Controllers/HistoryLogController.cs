using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCPL.Application.BusinessInterfaces;

namespace SCPL.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HistoryLogController : ControllerBase
    {
        private readonly IHistoryLogService _historyLogService;

        public HistoryLogController(IHistoryLogService historyLogService)
        {
            _historyLogService = historyLogService;
        }
        [Authorize]
        [HttpGet("GetLogs")]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString();

                var logs = await _historyLogService.GetLogsAsync(token);

                if (logs == null)
                {
                    return BadRequest("No logs found.");
                }

                return Ok(logs);
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
