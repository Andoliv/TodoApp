using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;
        private readonly IMapper _mapper;

        public TodoService(TodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoViewModel>> GetTodos()
        {
            var todos = await _context.Todos.ToListAsync();

            return _mapper.Map<IEnumerable<TodoViewModel>>(todos);
        }

        public async Task<TodoViewModel> GetTodoById(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            return _mapper.Map<TodoViewModel>(todo);
        }

        public async Task<TodoViewModel> CreateTodo(TodoViewModel todoViewModel)
        {
            var todo = _mapper.Map<Todo>(todoViewModel);

            _context.Add(todo);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoViewModel>(todo);
        }

        public async Task<TodoViewModel> UpdateTodo(TodoViewModel todoViewModel)
        {
            var todo = _context.Todos.FirstOrDefault(todo => todo.Id == todoViewModel.Id);

            if (todo == null)
            {
                throw new Exception("The ToDo you are looking for does not exist!");
            }

            if (TodoExists(todoViewModel.Id))
            {
                todo.Item = todoViewModel.Item;
                todo.IsCompleted = todoViewModel.IsCompleted;
                todo.DueOn = todoViewModel.DueOn;
                todo.UpdatedAt = DateTime.Now;
            }


            _context.Update(todo);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoViewModel>(todo); ;
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if (todo != null)
            {
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