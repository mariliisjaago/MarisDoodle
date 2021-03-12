using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Repos
{
    public interface IPollRepo
    {
        Task AddOptionsToPoll(int pollId, List<PollOptionModel> pollOptions);
        Task<int> CreateBasicPollAndReturnId(PollModel poll);
    }
}