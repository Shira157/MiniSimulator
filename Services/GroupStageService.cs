using MiniSimulator.Data;
using MiniSimulator.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using MiniSimulator.Services.Interface;
using MiniSimulator.Models.ViewModels;

namespace MiniSimulator.Services
{
    public class GroupStageService : IGroupStageService
    {
        private readonly ApplicationDbContext _context;

        public GroupStageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public GroupStage InitializeGroupStage()
        {
            var teams = _context.Team.ToList();

            return new GroupStage
            {
                Teams = teams,
                Matches = new List<Match>(),
                Standings = teams.Select(t => new TeamStats { Team = t }).ToList()
            };
        }

        //Simulates match results by assigning random scores influenced by team strengths. The home team is given an advantage (+10)
        public void SimulateMatches(GroupStage groupStage)
        {
            var matches = new List<Match>
        {
            new Match { HomeTeam = groupStage.Teams[0], AwayTeam = groupStage.Teams[1], Round = 1 },
            new Match { HomeTeam = groupStage.Teams[2], AwayTeam = groupStage.Teams[3], Round = 1 },
            new Match { HomeTeam = groupStage.Teams[0], AwayTeam = groupStage.Teams[2], Round = 2 },
            new Match { HomeTeam = groupStage.Teams[1], AwayTeam = groupStage.Teams[3], Round = 2 },
            new Match { HomeTeam = groupStage.Teams[0], AwayTeam = groupStage.Teams[3], Round = 3 },
            new Match { HomeTeam = groupStage.Teams[1], AwayTeam = groupStage.Teams[2], Round = 3 }
        };

            var random = new Random();
            foreach (var match in matches)
            {
                // Simulate match result based on team strength
                var homeStrength = match.HomeTeam.Strength + 10; // Home team gets +10 strength
                var awayStrength = match.AwayTeam.Strength;

                var totalStrength = homeStrength + awayStrength;
                var homeWinProbability = (double)homeStrength / totalStrength;

                if (random.NextDouble() < homeWinProbability)
                {
                    match.HomeScore = random.Next(1, 5); // Home team wins
                    match.AwayScore = random.Next(0, match.HomeScore); // Away team loses
                }
                else
                {
                    match.AwayScore = random.Next(1, 5); // Away team wins
                    match.HomeScore = random.Next(0, match.AwayScore); // Home team loses
                }

                groupStage.Matches.Add(match);
            }
        }


        //Calculates the performance metrics for each team, sorts them based on the defined criteria, and assigns positions accordingly.
        public List<Standing> CalculateStandings(GroupStage groupStage)
        {
            var standings = new List<Standing>();

            foreach (var team in groupStage.Teams)
            {
                
                var played = groupStage.Matches.Count(m => m.HomeTeam == team || m.AwayTeam == team); //Count the matches where the team is either the home or away team.
                var wins = groupStage.Matches.Count(m => (m.HomeTeam == team && m.HomeScore > m.AwayScore) || (m.AwayTeam == team && m.AwayScore > m.HomeScore)); //Count the matches where the team wins (either as home or away).
                var draws = groupStage.Matches.Count(m => m.HomeScore == m.AwayScore && (m.HomeTeam == team || m.AwayTeam == team)); // Count the matches where the score is tied, and the team is either the home or away team.
                var losses = played - wins - draws; //Subtract wins and draws from the total matches played.
                var goalsFor = groupStage.Matches.Where(m => m.HomeTeam == team).Sum(m => m.HomeScore) + groupStage.Matches.Where(m => m.AwayTeam == team).Sum(m => m.AwayScore); //Sum up the goals scored by the team as both home and away.
                var goalsAgainst = groupStage.Matches.Where(m => m.HomeTeam == team).Sum(m => m.AwayScore) + groupStage.Matches.Where(m => m.AwayTeam == team).Sum(m => m.HomeScore); //Sum up the goals conceded by the team as both home and away.
                var points = (wins * 3) + draws; //3 points per win and 1 point for draw;

                standings.Add(new Standing
                {
                    TeamName = team.Name,
                    Played = played,
                    Wins = wins,
                    Draws = draws,
                    Losses = losses,
                    GoalsFor = goalsFor,
                    GoalsAgainst = goalsAgainst,
                    Points = points
                });
            }

            // Sort standings
            standings = standings
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalsFor - s.GoalsAgainst)
                .ThenByDescending(s => s.GoalsFor)
                .ToList();

            // Assign position
            for (int i = 0; i < standings.Count; i++)
            {
                standings[i].Position = i + 1;
            }

            return standings;
        }
    }
}
