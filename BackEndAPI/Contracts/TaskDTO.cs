using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Contracts
{
    public class TaskDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool? IsComplete { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Range(1, 5)]
        public int Priority { get; set; }
        public int GoalId { get; set; }
        public TaskDTO()
        {
            IsComplete = false;
        }
    }
}