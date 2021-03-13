using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Repos
{
    public interface IOptionRepo
    {
        Task AddOptionsToPoll(int pollId, List<PollOptionModel> pollOptions);
        Task<List<PollOptionModel>> GetPollOptions(int pollId);
        Task DeleteOptionFromPoll(int optionId);
    }
}