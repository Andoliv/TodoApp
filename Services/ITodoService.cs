using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoViewModel>> GetTodos();

        Task<TodoViewModel> GetTodoById(int id);

        Task<TodoViewModel> CreateTodo(TodoViewModel todoViewModel);

        Task<TodoViewModel> UpdateTodo(TodoViewModel todoViewModel);

        Task DeleteTodo(int id);
        
        bool TodoExists(int id);
    }
}
