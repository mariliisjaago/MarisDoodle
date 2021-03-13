using MarisDoodleLibrary.Models;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Repos
{
    public interface IPollRepo
    {
        Task<int> CreateBasicPollAndReturnId(PollModel poll);

        Task<PollModel> GetBasicPoll(int id);
    }
}