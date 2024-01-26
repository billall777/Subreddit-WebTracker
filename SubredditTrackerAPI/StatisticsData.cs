namespace SubredditTrackerAPI
{
    public sealed class StatisticsData
    {
        private StatisticsData()
        {
            Posts = new List<PostMomento>();
            Authors = new List<AuthorMomento>();
        }


        private static StatisticsData instance = null;
        public List<PostMomento> Posts { get; set; }
        public List<AuthorMomento> Authors { get; set; }
        public List<string> Errors { get; set; }    


        public static StatisticsData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatisticsData();
                }
                return instance;
            }
        }

    }
}