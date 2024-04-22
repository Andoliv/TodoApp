using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetTodoById(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
        }

        public async Task<Todo> CreateTodo(Todo todo)
        {
            _context.Add(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo> UpdateTodo(Todo todo)
        {
            _context.Update(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await GetTodoById(id);

            if (todo != null) { 
                _context.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public bool TodoExists(int id)
        {
            return _context.Todos.Any(todo => todo.Id == id);
        }

    }
}
