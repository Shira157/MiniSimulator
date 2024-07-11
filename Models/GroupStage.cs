using System.Collections.Generic;

namespace MiniSimulator.Models
{
    public class GroupStage
    {
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
        public List<TeamStats> Standings { get; set; }
    }
}
