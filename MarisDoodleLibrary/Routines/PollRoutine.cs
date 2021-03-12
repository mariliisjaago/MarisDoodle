using MarisDoodleLibrary.Models;
using MarisDoodleLibrary.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Routines
{
    public class PollRoutine
    {
        private readonly SqlPollRepo _pollRepo;

        public PollRoutine(SqlPollRepo pollRepo)
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
