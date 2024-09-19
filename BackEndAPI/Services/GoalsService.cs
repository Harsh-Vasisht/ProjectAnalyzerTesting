using System.Collections;
using AutoMapper;
using BackEndAPI.AppDataContext;
using BackEndAPI.Contracts;
using BackEndAPI.Interfaces;
using BackEndAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Services
{
    public class GoalsService : IGoalsService
    {
        private readonly ToDoAPIDbContext _context;
        private readonly ILogger<GoalsService> _logger;
        private readonly IMapper _mapper;
        public GoalsService(ToDoAPIDbContext context, ILogger<GoalsService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Goal>> GetAllGoalsAsync(string userId)
        {
            var goals = await _context.Goals.Where(t => t.UserId == userId).ToListAsync();
            if(goals == null)
            {
                throw new Exception("No Goals Found.");
            }
            return goals;
        }
        public async Task CreateGoalAsync(GoalDTO request, string userId)
        {
            try
            {
                var goal = _mapper.Map<Goal>(request);
                goal.UserId = userId;
                _context.Goals.Add(goal);
                await _context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }
        public async Task UpdateGoalAsync(GoalDTO request, string userId, int goalId)
        {
            try
            {
                var goal = await _context.Goals.FirstOrDefaultAsync(t => t.GoalId == goalId && t.UserId == userId);
                if (goal == null)
                {
                    throw new Exception("Todo Item not found.");
                }
                goal.GoalTitle = request.GoalTitle;
                goal.GoalDescription = request.GoalDescription;
                goal.GoalStartDate = request.GoalStartDate;
                goal.GoalEndDate = request.GoalEndDate;
                goal.Status = request.Status;
                await _context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, "An error occurred while updating the Todo item.");
                throw new Exception("An error occurred while updating the Todo item.");
            }
        }
        public async Task DeleteGoalAsync(string userId, int goalId)
        {
            var goal = await _context.Goals.FirstOrDefaultAsync(t => t.GoalId == goalId && t.UserId == userId);
            if(goal == null)
            {
                throw new Exception("Todo Item not found.");
            }          
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
        }
    }
}
