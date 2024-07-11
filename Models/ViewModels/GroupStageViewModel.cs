using System.Collections.Generic;

namespace MiniSimulator.Models.ViewModels
{
    public class GroupStageViewModel
    {
        public List<Match> Matches { get; set; }
        public List<Standing> Standings { get; set; }
    }

    public class Standing
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get; set; }
    }
}
