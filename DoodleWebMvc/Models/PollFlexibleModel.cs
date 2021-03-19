using MarisDoodleLibrary.Models;
using System.Collections.Generic;

namespace DoodleWebMvc.Models
{
    public class PollFlexibleModel
    {
        public PollModel Poll { get; set; }

        public PollOptionModel NewOption { get; set; }
        public string RedirectingUrl { get; set; }
        public List<PollOptionModel> Options { get; set; } = new List<PollOptionModel>();
    }
}
