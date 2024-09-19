using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackEndAPI.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int? GoalId { get; set; }
        public Goal Goal { get; set; }
        public ToDo()
        {
            IsComplete = false;
        }
    }
}