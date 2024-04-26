using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp.Controllers;
public class TodoController : Controller
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var todoViewModels = await _todoService.GetTodos(1);

        if (todoViewModels == null)
        {
            return Problem("Entity set 'TodoDbContext.Todos' is null!");
        }

        return View(todoViewModels);
    }

    public async Task<IActionResult> DetailsAsync(int id)
    {
        var todoViewModel = await _todoService.GetTodoById(id);

        if (todoViewModel == null)
        {
            return NotFound();
        }

        return View(todoViewModel);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(TodoViewModel todoViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(todoViewModel);
        }

        await _todoService.CreateTodo(todoViewModel, 1);

        return RedirectToAction(nameof(Index));
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _todoService.GetTodos(1) == null)
        {
            return NotFound();
        }

        var todoViewModel = await _todoService.GetTodoById(id.Value);

        if (todoViewModel == null)
        {
            return NotFound();
        }

        return View(todoViewModel);
    }

    // POST: Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TodoViewModel todoViewModel)
    {
        if (id != todoViewModel.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(todoViewModel);
        }

        try
        {
            await _todoService.UpdateTodo(todoViewModel, 1);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_todoService.TodoExists(todoViewModel.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Todo/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _todoService.GetTodos == null)
        {
            return NotFound();
        }

        var todoViewModel = await _todoService.GetTodoById(id.Value);

        if (todoViewModel == null)
        {
            return NotFound();
        }

        return View(todoViewModel);
    }

    //POST: Todo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_todoService.GetTodos(1) == null)
        {
            return Problem("Entity set 'TodoDbContext' is null!");
        }

        var todoViewModel = await _todoService.GetTodoById(id);

        if (todoViewModel != null)
        {
            await _todoService.DeleteTodo(id);
        }

        return RedirectToAction(nameof(Index));
    }
}
