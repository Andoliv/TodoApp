using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodos();

        Task<Todo> GetTodoById(int id);

        Task<Todo> CreateTodo(Todo todo);

        Task<Todo> UpdateTodo(Todo todo);

        Task DeleteTodo(int id);
        
        bool TodoExists(int id);
    }
}
