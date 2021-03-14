using System;

namespace MarisDoodleLibrary.Models
{
    public class PollOptionVotingModel
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public int PollId { get; set; }
        public string VoterName { get; set; }
        public bool VotedFor { get; set; }
        public DateTime VotedOn { get; set; } = DateTime.UtcNow;
    }
}
