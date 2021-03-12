using System;

namespace MarisDoodleLibrary.Models
{
    public class PollOptionModel
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public int PollId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
