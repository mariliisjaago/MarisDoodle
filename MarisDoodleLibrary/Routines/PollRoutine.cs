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
        private readonly IOptionRepo _optionRepo;

        public PollRoutine(IPollRepo pollRepo, IOptionRepo optionRepo)
        {
            _pollRepo = pollRepo;
            _optionRepo = optionRepo;
        }

        public async Task<int> CreatePollAndReturnId(PollModel poll, List<PollOptionModel> options)
        {
            var pollId = await _pollRepo.CreateBasicPollAndReturnId(poll);

            await _optionRepo.AddOptionsToPoll(pollId, options);

            return pollId;
        }

        public async Task<int> CreateBasicPollAndReturnId(PollModel poll)
        {
            var pollId = await _pollRepo.CreateBasicPollAndReturnId(poll);

            return pollId;
        }

        public Task AddOptionsToPoll(int pollId, List<PollOptionModel> pollOptions)
        {
            return _optionRepo.AddOptionsToPoll(pollId, pollOptions);
        }

        public Task<PollModel> GetBasicPoll(int id)
        {
            return _pollRepo.GetBasicPoll(id);
        }

        public Task<List<PollOptionModel>> GetPollOptions(int pollId)
        {
            return _optionRepo.GetPollOptions(pollId);
        }

        public Task DeleteOption(int optionId)
        {
            return _optionRepo.DeleteOptionFromPoll(optionId);
        }
    }
}
