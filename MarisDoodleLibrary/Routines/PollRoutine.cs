using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Routines
{
    public class PollRoutine : IPollRoutine
    {
        private readonly IPollRepo _pollRepo;

        public PollRoutine(IPollRepo pollRepo)
        {
            _pollRepo = pollRepo;
        }

        public async Task<int> CreatePollAndReturnId(PollModel poll, List<PollOptionModel> options)
        {
            var pollId = await _pollRepo.CreateBasicPollAndReturnId(poll);

            await _pollRepo.AddOptionsToPoll(pollId, options);

            return pollId;
        }
    }
}
