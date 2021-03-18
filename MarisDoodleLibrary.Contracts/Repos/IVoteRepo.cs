using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Repos
{
    public interface IVoteRepo
    {
        Task SaveVotes(List<VoteModel> votes);
        Task<List<VoteModel>> GetVotesByOptionId(int optionId);
    }
}