using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodos();
    }
}
