using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Repos
{
    public interface IOptionRepo
    {
        Task AddOptionsToPoll(int pollId, List<PollOptionModel> pollOptions);
        Task<List<PollOptionModel>> GetPollOptionsForDisplay(int pollId);
        Task DeleteOptionFromPoll(int optionId);
        Task<List<PollOptionVotingModel>> GetPollOptionsForVoting(int pollId);
    }
}