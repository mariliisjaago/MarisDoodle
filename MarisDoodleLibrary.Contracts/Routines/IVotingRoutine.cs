using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Routines
{
    public interface IVotingRoutine
    {
        Task SaveVotes(List<VoteModel> votes);
    }
}