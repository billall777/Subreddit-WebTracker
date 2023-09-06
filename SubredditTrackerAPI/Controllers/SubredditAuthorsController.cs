using Microsoft.AspNetCore.Mvc;

namespace SubredditTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubredditAuthorsController : ControllerBase
    {
        private readonly ILogger<SubredditAuthorsController> _logger;

        public SubredditAuthorsController(ILogger<SubredditAuthorsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSubredditAuthors")]
        public IEnumerable<AuthorMomento> Get()
        {
            StatisticsData data = StatisticsData.Instance;
            return data.Authors.OrderByDescending(a => a.NewPostCount);
        }
    }
}
