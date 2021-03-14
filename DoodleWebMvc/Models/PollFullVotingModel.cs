using MarisDoodleLibrary.Models;
using System.Collections.Generic;

namespace DoodleWebMvc.Models
{
    public class PollFullVotingModel
    {
        public PollModel Poll { get; set; }
        public List<PollOptionVotingModel> Options { get; set; } = new List<PollOptionVotingModel>();
    }
}
