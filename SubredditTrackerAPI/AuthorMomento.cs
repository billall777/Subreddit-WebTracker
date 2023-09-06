namespace SubredditTrackerAPI
{
    public class AuthorMomento
    {
        public string Name { get; set; }
        public int NewPostCount { get; set; }
        public int PostCountAtStart { get; internal set; }
        public int PostCountLatest { get; internal set; }
    }
}