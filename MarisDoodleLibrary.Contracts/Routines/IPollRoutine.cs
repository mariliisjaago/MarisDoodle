using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Routines
{
    public interface IPollRoutine
    {
        Task<int> CreatePollAndReturnId(PollModel poll, List<PollOptionModel> options);
        Task<int> CreateBasicPollAndReturnId(PollModel poll);
        Task<PollModel> GetBasicPoll(int id);
        Task<List<PollOptionModel>> GetPollOptionsForDisplay(int pollId);
        Task<List<PollOptionVotingModel>> GetPollOptionsForVoting(int pollId);
        Task AddOptionsToPoll(int pollId, List<PollOptionModel> pollOptions);
        Task DeleteOption(int optionId);
    }
}