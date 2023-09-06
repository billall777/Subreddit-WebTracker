using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Reddit;
using Reddit.Controllers;

namespace SubredditTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitorSubredditController : ControllerBase
    {
        private readonly ILogger<MonitorSubredditController> _logger;
        private readonly IConfiguration _Config;

        public MonitorSubredditController(ILogger<MonitorSubredditController> logger, IConfiguration config)
        {
            _logger = logger;
            _Config = config;

        }

        [AllowAnonymous]
        [HttpGet(Name = "MonitorSubreddit")]
        public string Get()
        {
            _logger.LogInformation(message: $"MonitorSubreddit started at: {DateTime.Now}");

            var appId = _Config["AppId"];
            var refreshToken = _Config["refreshToken"];
            var accessToken = _Config["accessToken"];

            RedditClient reddit = new(appId: appId, refreshToken: refreshToken, accessToken: accessToken);
            Subreddit worldnews = reddit.Subreddit("worldnews");
            List<Post> newPosts = worldnews.Posts.GetNew();

            // Post UpVotes Statistics
            UpdatePostStats(newPosts);

            // Author Statistics
            UpdateAuthorStats(newPosts);

            _logger.LogInformation(message: $"MonitorSubreddit Ended at: {DateTime.Now}");
            return "Success";
        }

        private static void UpdatePostStats(List<Post> newPosts)
        {
            StatisticsData data = StatisticsData.Instance;
            foreach (Post p in newPosts)
            {
                var momento = data.Posts.Find(a => a.Id == p.Id);
                if (momento == null)
                    data.Posts.Add(new PostMomento { Id = p.Id, Title = p.Title, UpVotesAtStart = p.UpVotes, UpVotesLatest = p.UpVotes });
                else if (momento.UpVotesAtStart < p.UpVotes && momento.UpVotesLatest < p.UpVotes)
                {
                    momento.UpVotesLatest = p.UpVotes;
                }
            }
        }

        private static void UpdateAuthorStats(List<Post> newPosts)
        {
            StatisticsData data = StatisticsData.Instance;
            var myAuthors = (
                            from p in newPosts
                            group p by p.Author into pgroup
                            select new AuthorMomento { NewPostCount = pgroup.Count(), Name = pgroup.Key }
                        ).ToList();

            foreach (var p in myAuthors)
            {
                var momento = data.Authors.Find(a => a.Name == p.Name);
                if (momento == null)
                    data.Authors.Add(new AuthorMomento { Name = p.Name, NewPostCount = p.NewPostCount });
                else if (momento.PostCountAtStart < p.NewPostCount && momento.PostCountLatest < p.NewPostCount)
                {
                    momento.PostCountLatest = p.NewPostCount;
                }
            }
        }
    }
}
