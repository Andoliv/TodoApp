namespace TodoApp.Models;

public class Todo
{
    public int Id { get; set; }

    public required string Item { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? DueOn { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}