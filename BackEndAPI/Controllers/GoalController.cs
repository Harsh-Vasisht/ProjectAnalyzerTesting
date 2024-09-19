using BackEndAPI.Contracts;
using BackEndAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalController : ControllerBase
    {
        private readonly IGoalsService _goalsService;
        private readonly UserManager<IdentityUser> _userManager;
        public GoalController(IGoalsService goalsService, UserManager<IdentityUser> userManager)
        {
            _goalsService = goalsService;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllGoalsAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    var goals = await _goalsService.GetAllGoalsAsync(userId);
                    if (goals == null || !goals.Any())
                    {
                        return Ok(new { message = "No ToDo Items Found." });
                    }
                    return Ok(new { message = "Successfully retrieved all the Goals.", data = goals });
                }
                return Unauthorized();
            }
            catch(Exception exception)
            {
                return StatusCode(500, new { message = "An error occurred", error = exception.Message });
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateGoalAsync(GoalDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    await _goalsService.CreateGoalAsync(request, userId);
                    return Ok(new { message = "Blog post successfully created" });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the Goal", error = ex.Message });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateGoalAsync(GoalDTO request, int goalId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    await _goalsService.UpdateGoalAsync(request, userId, goalId);
                    return Ok(new { message = "Goal successfully updated" });
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the Goal", error = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteGoalAsync(int goalId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    await _goalsService.DeleteGoalAsync(userId, goalId);
                    return Ok();
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the Goal", error = ex.Message });
            }
        }
    }
}