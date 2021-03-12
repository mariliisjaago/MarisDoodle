using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Routines
{
    public interface IPollRoutine
    {
        Task<int> CreatePollAndReturnId(PollModel poll, List<PollOptionModel> options);
    }
}