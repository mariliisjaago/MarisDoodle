using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarisDoodleLibrary.Models
{
    public class PollModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must have at least 3 characters")]
        [MaxLength(250, ErrorMessage = "Must not exceed 250 characters")]
        [DisplayName("Name of your poll")]
        public string PollName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
