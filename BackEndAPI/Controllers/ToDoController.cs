using BackEndAPI.Contracts;
using BackEndAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoServices _toDoServices;
        private readonly UserManager<IdentityUser> _userManager;
        public ToDoController(IToDoServices toDoServices, UserManager<IdentityUser> userManager)
        {
            _toDoServices = toDoServices;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    var toDos = await _toDoServices.GetAllAsync(userId);
                    if (toDos == null || !toDos.Any())
                    {
                        return Ok(new { message = "No ToDo Items Found." });
                    }
                    return Ok(new { message = "Successfully retrieved all the To Do Tasks.", data = toDos });
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
        public async Task<IActionResult> CreateTodoAsync(TaskDTO request)
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
                    await _toDoServices.CreateToDoAsync(request, userId);
                    return Ok(new { message = "Blog post successfully created" });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateToDoAsync(int id, TaskDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    await _toDoServices.UpdateToDoAsync(id, request, userId);
                    return Ok(new { message = "Blog post successfully updated" });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the updating Todo Item", error = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    await _toDoServices.DeleteToDoAsync(id, userId);
                    return Ok(new { message = "Blog post successfully deleted" });
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the Todo Item", error = ex.Message });
            }
        }
    }
}