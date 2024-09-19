using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Contracts
{
    public class GoalDTO
    {
        [Required]
        [StringLength(100)]
        public string GoalTitle { get; set; }
        [StringLength(500)]
        public string GoalDescription { get; set; }
        [Required]
        public DateTime GoalStartDate { get; set; }
        [Required]
        public DateTime GoalEndDate { get; set; }
        [Required]
        public string Status { get; set; }
    }
}