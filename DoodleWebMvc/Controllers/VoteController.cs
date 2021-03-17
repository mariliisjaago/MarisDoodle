using DoodleWebMvc.Models;
using DoodleWebMvc.Utils.Contracts;
using MarisDoodleLibrary.Contracts.Routines;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoodleWebMvc.Controllers
{
    public class VoteController : Controller
    {
        private readonly IPollRoutine _pollRoutine;
        private readonly IModelPopulator _modelPopulator;

        public VoteController(IPollRoutine pollRoutine, IModelPopulator modelPopulator)
        {
            _pollRoutine = pollRoutine;
            _modelPopulator = modelPopulator;
        }

        public async Task<IActionResult> Index(int id)
        {
            PollFullVotingModel displayModel = await _modelPopulator.PopulatePollAndOptionsForVoting(id);

            return View(displayModel);
        }

        public IActionResult Vote(PollFullVotingModel pollVotingModel)
        {
            int id = pollVotingModel.Poll.Id;

            return RedirectToAction("Index", new { id });
        }
    }
}
