using System;

namespace MarisDoodleLibrary.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public string VoterName { get; set; }
        public int OptionId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
