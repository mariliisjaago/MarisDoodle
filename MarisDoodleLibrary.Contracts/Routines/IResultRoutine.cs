using MarisDoodleLibrary.Models;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Routines
{
    public interface IResultRoutine
    {
        Task<PollVotingResultModel> GetVotingResults(int pollId);
    }
}