using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackEndAPI.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public DateTime GoalStartDate { get; set; }
        public DateTime GoalEndDate { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
