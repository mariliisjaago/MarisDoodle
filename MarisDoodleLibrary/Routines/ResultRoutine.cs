using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
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

            pollResults.Poll = await _pollRepo.GetBasicPoll(pollId);

            var tempOptions = await _optionRepo.GetPollOptionsForDisplay(pollId);

            foreach (var item in tempOptions)
            {
                OptionWithVotesModel optionWithVotes = new OptionWithVotesModel();

                optionWithVotes.Option = item;

                optionWithVotes.Votes = await _voteRepo.GetVotesByOptionId(item.Id);

                pollResults.Options.Add(optionWithVotes);
            }

            return pollResults;
        }
    }
}
