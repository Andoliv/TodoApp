using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class RegisterViewModel : LoginViewModel
{
    public string? Name { get; set; }
}
