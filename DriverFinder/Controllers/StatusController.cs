using DriverFinder.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverFinder.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : Controller
    {
        private readonly IRequestLimiter _requestLimiter;

        public StatusController(IRequestLimiter requestLimiter)
        {
            _requestLimiter = requestLimiter;
        }

        [HttpGet("requests")]
        public IActionResult GetRequestStatus()
        {
            return Ok(new
            {
                CurrentRequests = _requestLimiter.GetCurrentRequests(),
                Limit = _requestLimiter.GetLimit(),
                Available = _requestLimiter.GetLimit() - _requestLimiter.GetCurrentRequests()
            });
        }
    }
}
