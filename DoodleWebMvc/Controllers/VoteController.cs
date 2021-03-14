using DoodleWebMvc.Models;
using MarisDoodleLibrary.Contracts.Routines;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoodleWebMvc.Controllers
{
    public class VoteController : Controller
    {
        private readonly IPollRoutine _pollRoutine;

        public VoteController(IPollRoutine pollRoutine)
        {
            _pollRoutine = pollRoutine;
        }

        public async Task<IActionResult> Index(int id)
        {
            PollFullVotingModel displayModel = new PollFullVotingModel();

            displayModel.Poll = await _pollRoutine.GetBasicPoll(id);
            displayModel.Options = await _pollRoutine.GetPollOptionsForVoting(id);

            return View(displayModel);
        }

        public IActionResult Vote(PollFullVotingModel pollVotingModel)
        {
            var temp = pollVotingModel;

            int id = pollVotingModel.Poll.Id;

            return RedirectToAction("Index", new { id });
        }
    }
}
