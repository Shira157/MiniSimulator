namespace MiniSimulator.Models
{
    public class TeamStats
    {
        public Team Team { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference => GoalsFor - GoalsAgainst;
    }
}
