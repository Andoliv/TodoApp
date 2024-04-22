using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
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
        var todos = await _todoService.GetTodos();

        if (todos == null)
        {
            return Problem("Entity set 'TodoDbContext.Todos' is null!");
        }

        var todosResponse = todos.Select(todo => new TodoRequest
        {
            Id = todo.Id,
            Item = todo.Item,
            IsCompleted = todo.IsCompleted,
            DueOn = todo.DueOn
        });

        return View(todosResponse);
    }

    public async Task<IActionResult> DetailsAsync(int id)
    {
        var todo = await _todoService.GetTodoById(id);

        if(todo == null)
        {
            return NotFound();
        }

        var todoResponse = new TodoRequest
        {
            Id = todo.Id,
            Item = todo.Item,
            IsCompleted = todo.IsCompleted,
            DueOn = todo.DueOn
        };

        return View(todoResponse);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(TodoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);            
        }

        var todo = new Todo
        {
            Item = request.Item,
            IsCompleted = request.IsCompleted,
            DueOn = request.DueOn,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _todoService.CreateTodo(todo);

        return RedirectToAction(nameof(Index));
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _todoService.GetTodos() == null)
        {
            return NotFound();
        }

        var todo = await _todoService.GetTodoById(id.Value);

        if(todo == null)
        {
            return NotFound();
        }

        var todoResponse = new TodoRequest
        {
            Id = todo.Id,
            Item = todo.Item,
            IsCompleted = todo.IsCompleted,
            DueOn = todo.DueOn
        };

        return View(todoResponse);
    }

    // POST: Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TodoRequest todoRequest)
    {
        if (id != todoRequest.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(todoRequest);
        }

        var todo = new Todo
        {
            Id = todoRequest.Id,
            Item = todoRequest.Item,
            IsCompleted = todoRequest.IsCompleted,
            DueOn = todoRequest.DueOn,
            UpdatedAt = DateTime.Now
        };

        try
        {
            await _todoService.UpdateTodo(todo);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_todoService.TodoExists(todo.Id))
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

        var todo = await _todoService.GetTodoById(id.Value);

        if (todo == null)
        {
            return NotFound();
        }

        var todoResponse = new TodoRequest
        {
            Id = todo.Id,
            Item = todo.Item,
            IsCompleted = todo.IsCompleted,
            DueOn = todo.DueOn
        };

        return View(todoResponse);
    }

    //POST: Todo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if(_todoService.GetTodos() == null)
        {
            return Problem("Entity set 'TodoDbContext' is null!");
        }

        var todo = await _todoService.GetTodoById(id);

        if (todo != null)
        {
            await _todoService.DeleteTodo(id);
        }

        return RedirectToAction(nameof(Index));
    }
}
