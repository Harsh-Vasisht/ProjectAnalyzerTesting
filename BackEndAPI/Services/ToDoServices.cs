using AutoMapper;
using BackEndAPI.AppDataContext;
using BackEndAPI.Contracts;
using BackEndAPI.Interfaces;
using BackEndAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Services
{
    public class ToDoServices : IToDoServices
    {
        private readonly ToDoAPIDbContext _context;
        private readonly ILogger<ToDoServices> _logger;
        private readonly IMapper _mapper;
        public ToDoServices(ToDoAPIDbContext context, ILogger<ToDoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task CreateToDoAsync(TaskDTO request, string userId)
        {
            try
            {
                var toDo = _mapper.Map<ToDo>(request);
                toDo.CreatedAt = DateTime.UtcNow;
                toDo.UpdatedAt = DateTime.UtcNow;
                toDo.UserId = userId;
                _context.toDos.Add(toDo);
                await _context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        public async Task UpdateToDoAsync(int Id, TaskDTO request, string userId)
        {
            try
            {
                var todo = await _context.toDos.FirstOrDefaultAsync(t => t.Id == Id && t.UserId == userId);
                if (todo == null)
                {
                    throw new Exception("Todo Item not found.");
                }
                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }
                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }
                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }
                todo.DueDate = request.DueDate;
                todo.Priority = request.Priority;
                todo.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while updating the Todo item.");
                throw new Exception("An error occurred while updating the Todo item.");
            }
        }

        public async Task DeleteToDoAsync(int Id, string userId)
        {
            var todo = await _context.toDos.FirstOrDefaultAsync(t => t.Id == Id && t.UserId == userId);
            if(todo == null)
            {
                throw new Exception("Todo Item not found.");
            }          
            _context.toDos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public Task<ToDo> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToDo>> GetAllAsync(string userId)
        {
            var toDos = await _context.toDos.Where(t => t.UserId == userId).ToListAsync();
            if(toDos == null)
            {
                throw new Exception("No ToDo Items Found.");
            }
            return toDos;
        }
    }
}