using DoodleWebMvc.Models;
using System.Threading.Tasks;

namespace DoodleWebMvc.Utils.Contracts
{
    public interface IModelPopulator
    {
        Task<PollFullModel> PopulatePollAndOptionsForDisplay(int pollId);
        Task<PollFullVotingModel> PopulatePollAndOptionsForVoting(int pollId);
    }
}