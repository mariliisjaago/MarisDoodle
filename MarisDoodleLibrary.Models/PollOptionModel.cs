using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarisDoodleLibrary.Models
{
    public class PollOptionModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must have at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Must not exceed 100 characters")]
        [DisplayName("Poll option")]
        public string Option { get; set; }

        public int PollId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
