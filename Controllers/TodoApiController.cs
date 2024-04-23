using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoApiController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoApiController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: api/todos
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodos()
    {
        try
        {
            var todos = await _todoService.GetTodos();

            return Ok(todos);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoById([FromRoute] int id)
    {
        try
        {
            var todoViewModel = await _todoService.GetTodoById(id);

            if (todoViewModel == null)
            {
                return NotFound("The ToDo you are looking for does not exist!");
            }

            return Ok(todoViewModel);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

    }

    // POST: api/todos
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodo([FromBody] TodoViewModel todoViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTodoViewModel = await _todoService.CreateTodo(todoViewModel);

            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodoViewModel.Id }, createdTodoViewModel);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

    }

    // PUT: api/todos/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] TodoViewModel todoViewModel)
    {
        try
        {
            if (todoViewModel.Id == null)
            {
                todoViewModel.Id = id;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_todoService.TodoExists(id))
            {
                return NotFound("The ToDo you are looking for does not exist!");
            }

            var updatedTodoViewModel = await _todoService.UpdateTodo(todoViewModel);

            return Ok(updatedTodoViewModel);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodoById([FromRoute] int id)
    {
        try
        {
            if (!_todoService.TodoExists(id))
            {
                return NotFound("The ToDo you are looking for does not exist!");
            }

            await _todoService.DeleteTodo(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

    }

}
