using System.Collections.Generic;

namespace MarisDoodleLibrary.Models
{
    public class OptionWithVotesModel
    {
        public PollOptionModel Option { get; set; }
        public List<VoteModel> Votes { get; set; } = new List<VoteModel>();
    }
}
