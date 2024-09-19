using BackEndAPI.Contracts;
using BackEndAPI.Models;

namespace BackEndAPI.Interfaces
{
    public interface IToDoServices
    {
        Task<IEnumerable<ToDo>> GetAllAsync(string userId);
        Task<ToDo> GetByIdAsync(int id);
        Task CreateToDoAsync(TaskDTO request, string userId);
        Task UpdateToDoAsync(int Id, TaskDTO request, string userId);
        Task DeleteToDoAsync(int Id, string userId);
    }
}