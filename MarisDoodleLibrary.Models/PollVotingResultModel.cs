using System.Collections.Generic;

namespace MarisDoodleLibrary.Models
{
    public class PollVotingResultModel
    {
        public PollModel Poll { get; set; }

        public List<OptionWithVotesModel> Options { get; set; } = new List<OptionWithVotesModel>();
    }
}
