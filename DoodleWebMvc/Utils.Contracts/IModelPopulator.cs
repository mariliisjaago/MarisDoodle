using DoodleWebMvc.Models;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoodleWebMvc.Utils.Contracts
{
    public interface IModelPopulator
    {
        Task<PollFlexibleModel> PopulatePollAndOptionsForDisplay(int pollId);
        Task<PollVotingModel> PopulatePollAndOptionsForVoting(int pollId);
        List<VoteModel> TransformRawOptionDataToVotes(string voterName, List<PollOptionVotingModel> options);
        Task<PollFlexibleModel> PopulatePollName(int id);
    }
}