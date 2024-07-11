namespace MiniSimulator.Models
{
    public class Match
    {
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Round { get; set; }
    }
}
