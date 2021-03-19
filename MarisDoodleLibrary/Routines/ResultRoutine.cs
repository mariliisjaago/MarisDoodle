using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Routines
{
    public class ResultRoutine : IResultRoutine
    {
        private readonly IPollRepo _pollRepo;
        private readonly IOptionRepo _optionRepo;
        private readonly IVoteRepo _voteRepo;

        public ResultRoutine(IPollRepo pollRepo, IOptionRepo optionRepo, IVoteRepo voteRepo)
        {
            _pollRepo = pollRepo;
            _optionRepo = optionRepo;
            _voteRepo = voteRepo;
        }

        public async Task<PollVotingResultModel> GetVotingResults(int pollId)
        {
            PollVotingResultModel pollResults = new PollVotingResultModel();

            pollResults = await GetPollBasicInfoAndAddToModel(pollId, pollResults);

            pollResults = await GetOptionsAndVotesFromDatabaseAndAddToModel(pollId, pollResults);

            // calculate stufF?

            return pollResults;
        }

        private async Task<PollVotingResultModel> GetOptionsAndVotesFromDatabaseAndAddToModel(int pollId, PollVotingResultModel pollResults)
        {
            var tempOptions = await _optionRepo.GetPollOptionsForDisplay(pollId);

            if (tempOptions != null)
            {
                pollResults = await AddOptionsAndVotesToModel(pollResults, tempOptions);
            }

            return pollResults;
        }

        private async Task<PollVotingResultModel> AddOptionsAndVotesToModel(PollVotingResultModel pollResults, List<PollOptionModel> tempOptions)
        {
            foreach (var item in tempOptions)
            {
                OptionWithVotesModel optionWithVotes = new OptionWithVotesModel()
                {
                    Option = item
                };

                var tempVotes = await _voteRepo.GetVotesByOptionId(item.Id);

                if (tempVotes != null)
                {
                    optionWithVotes.Votes = tempVotes;
                }

                pollResults.Options.Add(optionWithVotes);
            }

            return pollResults;
        }

        private async Task<PollVotingResultModel> GetPollBasicInfoAndAddToModel(int pollId, PollVotingResultModel pollResults)
        {
            var tempPoll = await _pollRepo.GetBasicPoll(pollId);

            if (tempPoll == null)
            {
                pollResults.Poll = new PollModel();
            }
            else
            {
                pollResults.Poll = tempPoll;
            }

            return pollResults;
        }
    }
}
