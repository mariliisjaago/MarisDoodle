using DoodleWebMvc.Models;
using DoodleWebMvc.Utils.Contracts;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IUrlGenerator _urlGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VoteController(IPollRoutine pollRoutine, IVotingRoutine votingRoutine,
                                IModelPopulator modelPopulator, IUrlGenerator urlGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _pollRoutine = pollRoutine;
            _votingRoutine = votingRoutine;
            _modelPopulator = modelPopulator;
            _urlGenerator = urlGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(int id)
        {
            PollVotingModel displayModel = await _modelPopulator.PopulatePollAndOptionsForVoting(id);

            return View(displayModel);
        }

        public async Task<IActionResult> Vote(PollVotingModel pollVotingModel)
        {
            int id = pollVotingModel.Poll.Id;

            if (ModelState.IsValid)
            {
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

            return RedirectToAction("Index", new { id });
        }

        public async Task<IActionResult> ErrorVoting(int id)
        {
            PollFlexibleModel displayModel = await _modelPopulator.PopulatePollName(id);

            displayModel.RedirectingUrl = _urlGenerator.GetVotingPageUrl(id, Url, _httpContextAccessor);

            return View(displayModel);
        }

        public async Task<IActionResult> SuccessVoting(int id)
        {
            PollFlexibleModel displayModel = await _modelPopulator.PopulatePollName(id);

            displayModel.RedirectingUrl = _urlGenerator.GetResultsPageUrl(id, Url, _httpContextAccessor);

            return View(displayModel);
        }
    }
}
