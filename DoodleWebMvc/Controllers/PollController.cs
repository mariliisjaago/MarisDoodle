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
    public class PollController : Controller
    {
        private readonly IPollRoutine _pollRoutine;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModelPopulator _modelPopulator;
        private readonly IUrlGenerator _urlGenerator;

        //private string optionName;

        public PollController(IPollRoutine pollRoutine, IHttpContextAccessor httpContextAccessor,
                                IModelPopulator modelPopulator, IUrlGenerator urlGenerator)
        {
            _pollRoutine = pollRoutine;
            _httpContextAccessor = httpContextAccessor;
            _modelPopulator = modelPopulator;
            _urlGenerator = urlGenerator;
        }

        public IActionResult Index()
        {
            PollModel model = new PollModel();

            return View(model);
        }

        public async Task<IActionResult> Display(int id)
        {
            PollFlexibleModel displayModel = await _modelPopulator.PopulatePollAndOptionsForDisplay(id);

            return View(displayModel);
        }

        [HttpPost]
        public async Task<IActionResult> GivePollName(PollModel poll)
        {
            int id = await _pollRoutine.CreateBasicPollAndReturnId(poll);

            return RedirectToAction("Display", new { id });
        }

        [HttpPost]
        public IActionResult AddOption(PollFlexibleModel pollFullModel)
        {
            int id = pollFullModel.Poll.Id;

            _pollRoutine.AddOptionsToPoll(
                id,
                new List<PollOptionModel>
                {
                    new PollOptionModel
                    {
                        Option = pollFullModel.NewOption.Option
                    }
                }
            );

            return RedirectToAction("Display", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOption(int pollId, int optionId)
        {
            await _pollRoutine.DeleteOption(optionId);

            int id = pollId;

            return RedirectToAction("Display", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Done(PollFlexibleModel pollFullModel)
        {
            int id = pollFullModel.Poll.Id;

            PollFlexibleModel displayModel = await _modelPopulator.PopulatePollAndOptionsForDisplay(id);

            displayModel.RedirectingUrl = _urlGenerator.GetVotingPageUrl(id, Url, _httpContextAccessor);

            return View(displayModel);
        }

    }
}
