using Microsoft.AspNetCore.Mvc;

namespace SubredditTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubredditVotesController : ControllerBase
    {
        private readonly ILogger<SubredditVotesController> _logger;

        public SubredditVotesController(ILogger<SubredditVotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSubredditVotes")]
        public IEnumerable<PostMomento> Get()
        {
            StatisticsData data = StatisticsData.Instance;
            return data.Posts.OrderByDescending(a => a.NewVotes);
        }
    }
}
