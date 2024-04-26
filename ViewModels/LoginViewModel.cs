using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class LoginViewModel
{
    [Required]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters long")]
    public string Username { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }

}
