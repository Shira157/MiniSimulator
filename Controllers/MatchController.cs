using Microsoft.AspNetCore.Mvc;
using MiniSimulator.Models.ViewModels;
using MiniSimulator.Services.Interface;
using System.Linq;

namespace MiniSimulator.Controllers
{
    public class MatchController : Controller
    {
        private readonly IGroupStageService _groupStageService;

        public MatchController(IGroupStageService groupStageService)
        {
            _groupStageService = groupStageService;
        }
        public IActionResult Index()
        {
            var groupStage = _groupStageService.InitializeGroupStage();
            _groupStageService.SimulateMatches(groupStage);
            var standings = _groupStageService.CalculateStandings(groupStage);

            var viewModel = new GroupStageViewModel
            {
                Matches = groupStage.Matches.OrderBy(m => m.Round).ToList(), // Ensure matches are ordered by round,
                Standings = standings
            };
            return View(viewModel);
         
        }
    }
}
