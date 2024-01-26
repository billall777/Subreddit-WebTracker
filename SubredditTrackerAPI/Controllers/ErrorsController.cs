using Microsoft.AspNetCore.Mvc;

namespace SubredditTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<SubredditVotesController> _logger;

        public ErrorsController(ILogger<SubredditVotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetErrors")]
        public IEnumerable<string> Get()
        {
            StatisticsData data = StatisticsData.Instance;
            return data.Errors;
        }
    }
}
