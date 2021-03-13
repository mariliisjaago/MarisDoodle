using DoodleWebMvc.Models;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoodleWebMvc.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRoutine _pollRoutine;

        //private string optionName;

        public PollController(IPollRoutine pollRoutine)
        {
            _pollRoutine = pollRoutine;
        }

        public IActionResult Index()
        {
            PollModel model = new PollModel();

            return View(model);
        }

        public async Task<IActionResult> Display(int id)
        {
            PollFullModel displayModel = new PollFullModel();
            displayModel.Poll = await _pollRoutine.GetBasicPoll(id);
            displayModel.Options = await _pollRoutine.GetPollOptions(id);

            return View(displayModel);
        }

        [HttpPost]
        public async Task<IActionResult> GivePollName(PollModel poll)
        {
            int id = await _pollRoutine.CreateBasicPollAndReturnId(poll);

            return RedirectToAction("Display", new { id });
        }

        [HttpPost]
        public IActionResult AddOption(PollFullModel pollFullModel)
        {
            int id = pollFullModel.Poll.Id;

            _pollRoutine.AddOptionsToPoll(id,
                                          new List<PollOptionModel>
                                          {
                                              new PollOptionModel
                                              { Option = pollFullModel.NewOption.Option }
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
        public async Task<IActionResult> Done(PollFullModel pollFullModel)
        {
            int id = pollFullModel.Poll.Id;

            PollFullModel displayModel = new PollFullModel();
            displayModel.Poll = await _pollRoutine.GetBasicPoll(id);
            displayModel.Options = await _pollRoutine.GetPollOptions(id);

            return View(displayModel);
        }
    }
}
