using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var todos = await _todoService.GetTodos();

            return todos != null ? View(todos) : Problem("Entity set 'TodoDbContext.Todos' is null!");
        }
    }
}
