using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Routines
{
    public class VotingRoutine : IVotingRoutine
    {
        private readonly IPollRepo _pollRepo;
        private readonly IOptionRepo _optionRepo;
        private readonly IVoteRepo _voteRepo;

        public VotingRoutine(IPollRepo pollRepo, IOptionRepo optionRepo, IVoteRepo voteRepo)
        {
            _pollRepo = pollRepo;
            _optionRepo = optionRepo;
            _voteRepo = voteRepo;
        }

        public Task SaveVotes(List<VoteModel> votes)
        {
            return _voteRepo.SaveVotes(votes);
        }

    }
}
