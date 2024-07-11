using MiniSimulator.Models;
using MiniSimulator.Models.ViewModels;
using System.Collections.Generic;

namespace MiniSimulator.Services.Interface
{
    public interface IGroupStageService
    {
        GroupStage InitializeGroupStage();
        void SimulateMatches(GroupStage groupStage);
        List<Standing> CalculateStandings(GroupStage groupStage);
    }
}
