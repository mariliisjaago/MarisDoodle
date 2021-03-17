using DoodleWebMvc.Models;
using DoodleWebMvc.Utils.Contracts;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoodleWebMvc.Controllers
{
    public class VoteController : Controller
    {
        private readonly IPollRoutine _pollRoutine;
        private readonly IVotingRoutine _votingRoutine;
        private readonly IModelPopulator _modelPopulator;

        public VoteController(IPollRoutine pollRoutine, IVotingRoutine votingRoutine, IModelPopulator modelPopulator)
        {
            _pollRoutine = pollRoutine;
            _votingRoutine = votingRoutine;
            _modelPopulator = modelPopulator;
        }

        public async Task<IActionResult> Index(int id)
        {
            PollFullVotingModel displayModel = await _modelPopulator.PopulatePollAndOptionsForVoting(id);

            return View(displayModel);
        }

        public async Task<IActionResult> Vote(PollFullVotingModel pollVotingModel)
        {
            int id = pollVotingModel.Poll.Id;

            List<VoteModel> votes = _modelPopulator.TransformRawOptionDataToVotes(pollVotingModel.VoterName, pollVotingModel.Options);

            if (votes.Count == 0)
            {
                return RedirectToAction("ErrorVoting", new { id });
            }
            else
            {
                await _votingRoutine.SaveVotes(votes);

                return RedirectToAction("SuccessVoting", new { id });
            }
        }

        public IActionResult ErrorVoting(int id)
        {
            return View();
        }

        public async Task<IActionResult> SuccessVoting(int id)
        {
            PollFullModel displayModel = await _modelPopulator.PopulatePollName(id);

            return View(displayModel);
        }
    }
}
