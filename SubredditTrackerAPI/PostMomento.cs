namespace SubredditTrackerAPI
{
    public class PostMomento
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int UpVotesAtStart { get; set; }
        public int UpVotesLatest { get; set; }
        public int NewVotes { get { return (UpVotesLatest - UpVotesAtStart); } }
    }
}