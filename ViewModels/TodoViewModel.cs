using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class TodoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "You must input a Item name!")]
    [MinLength(3, ErrorMessage = "Item name must be at least 3 characters long!")]
    public required string Item { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? DueOn { get; set; }

}
