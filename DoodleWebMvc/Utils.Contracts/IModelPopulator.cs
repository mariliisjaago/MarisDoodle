using DoodleWebMvc.Models;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoodleWebMvc.Utils.Contracts
{
    public interface IModelPopulator
    {
        Task<PollFullModel> PopulatePollAndOptionsForDisplay(int pollId);
        Task<PollFullVotingModel> PopulatePollAndOptionsForVoting(int pollId);
        List<VoteModel> TransformRawOptionDataToVotes(string voterName, List<PollOptionVotingModel> options);
        Task<PollFullModel> PopulatePollName(int id);
    }
}