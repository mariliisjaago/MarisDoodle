using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoodleWebMvc.Models
{
    public class PollVotingModel
    {
        public PollModel Poll { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Must have at least 3 characters")]
        [MaxLength(50, ErrorMessage = "Must not exceed 50 characters")]
        [DisplayName("Enter your name")]
        public string VoterName { get; set; }
        public List<PollOptionVotingModel> Options { get; set; } = new List<PollOptionVotingModel>();
    }
}
