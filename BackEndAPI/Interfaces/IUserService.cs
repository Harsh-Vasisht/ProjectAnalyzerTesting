using BackEndAPI.Contracts;
using Microsoft.AspNetCore.Identity;

namespace BackEndAPI.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(UserDTO user);
        Task<string> LoginAsync(UserDTO user);
    }
}