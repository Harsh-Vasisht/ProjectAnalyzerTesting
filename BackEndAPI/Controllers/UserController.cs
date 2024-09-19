using BackEndAPI.Contracts;
using BackEndAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.RegisterAsync(user);
                if(result.Succeeded)
                {
                    return Ok(new { Message = "User Registered Successfully"});
                }
                throw new Exception("Something Went Wrong");
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", error = ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var token = await _userService.LoginAsync(user);
                if(token != null)
                {
                    return Ok(new { Token = token});
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", error = ex.Message});
            }
        }
    }
}