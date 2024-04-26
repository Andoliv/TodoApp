using TodoApp.ViewModels;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoViewModel>> GetTodos(int userId);

        Task<TodoViewModel> GetTodoById(int id);

        Task<TodoViewModel> CreateTodo(TodoViewModel todoViewModel, int userId);

        Task<TodoViewModel> UpdateTodo(TodoViewModel todoViewModel, int userId);

        Task DeleteTodo(int id);
        
        bool TodoExists(int id);
    }
}
