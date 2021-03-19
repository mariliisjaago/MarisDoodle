using DoodleWebMvc.Models;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoodleWebMvc.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultRoutine _resultRoutine;

        public ResultController(IResultRoutine resultRoutine)
        {
            _resultRoutine = resultRoutine;
        }

        public async Task<IActionResult> Index(int id)
        {
            PollResultsForDisplay displayModel = new PollResultsForDisplay();

            displayModel.PollResults = await _resultRoutine.GetVotingResults(id);

            displayModel.VoterNamesPerOption = CompileVoterNamesToConcatStrings(displayModel.PollResults.Options);

            return View(displayModel);
        }

        private List<string> CompileVoterNamesToConcatStrings(List<OptionWithVotesModel> options)
        {
            List<string> voterNamesPerOption = new List<string>();

            foreach (var option in options)
            {
                List<string> voterNames = ListVoterNames(option);

                string voterNamesConcat = string.Join(", ", voterNames);

                voterNamesPerOption.Add(voterNamesConcat);
            }

            return voterNamesPerOption;
        }

        private List<string> ListVoterNames(OptionWithVotesModel option)
        {
            List<string> output = new List<string>();
            
            foreach (var vote in option.Votes)
            {
                output.Add(vote.VoterName);
            }

            return output;
        }
    }
}
