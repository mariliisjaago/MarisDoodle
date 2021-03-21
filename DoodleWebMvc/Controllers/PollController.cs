using DoodleWebMvc.Models;
using DoodleWebMvc.Utils.Contracts;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            if (ModelState.IsValid == true)
            {
                int id = await _pollRoutine.CreateBasicPollAndReturnId(poll);

                return RedirectToAction("Display", new { id });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddOption(PollFlexibleModel pollFlexibleModel)
        {
            int id = pollFlexibleModel.Poll.Id;

            if (ModelState.IsValid == true)
            {
                bool haveRoomForNewOptions = await CheckForAvailableOptionSpace(id);

                if (haveRoomForNewOptions == true)
                {
                    await AddOneOptionToPoll(id, pollFlexibleModel.NewOption);
                }

                return RedirectToAction("Display", new { id });
            }

            return RedirectToAction("Display", new { id });
        }

        private Task AddOneOptionToPoll(int pollId, PollOptionModel newOption)
        {
            _pollRoutine.AddOptionsToPoll(
                            pollId,
                            new List<PollOptionModel>
                            {
                                new PollOptionModel
                                {
                                    Option = newOption.Option
                                }
                            }
                    );

            return Task.CompletedTask;
        }

        private async Task<bool> CheckForAvailableOptionSpace(int id)
        {
            var options = await _pollRoutine.GetPollOptionsForDisplay(id);

            if (options.Count < 20)
            {
                return true;
            }
            else
            {
                return false;
            }
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
