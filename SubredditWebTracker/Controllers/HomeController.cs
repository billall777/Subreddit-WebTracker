using Microsoft.AspNetCore.Mvc;
using Reddit;
using Reddit.Controllers;
using SubredditWebTracker.Models;
using System.Diagnostics;

namespace SubredditWebTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;

        }


        public IActionResult Index()
        {
            swaggerClient srvc = new swaggerClient("https://redditstatisticsapi.azurewebsites.net/", new System.Net.Http.HttpClient());
            var myPostsMomento = srvc.GetSubredditVotesAsync().Result;
            return View(myPostsMomento);
        }

        public IActionResult Privacy()
        {
            swaggerClient srvc = new swaggerClient("https://redditstatisticsapi.azurewebsites.net/", new System.Net.Http.HttpClient());
            var myAuthors = srvc.GetSubredditAuthorsAsync().Result;
            return View(myAuthors);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}