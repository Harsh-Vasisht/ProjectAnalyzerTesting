using BackEndAPI.Contracts;
using BackEndAPI.Models;

namespace BackEndAPI.Interfaces
{
    public interface IGoalsService
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync(string userId);
        Task CreateGoalAsync(GoalDTO goal, string userId);
        Task UpdateGoalAsync(GoalDTO goal, string userId, int GoalId);
        Task DeleteGoalAsync(string userId, int goalId);
    }
}
