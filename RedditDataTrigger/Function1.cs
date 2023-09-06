using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RedditDataTrigger
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("*/2 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://redditstatisticsapi.azurewebsites.net/MonitorSubreddit");
                var res = client.GetAsync("").Result;

            }
        }
    }
}
