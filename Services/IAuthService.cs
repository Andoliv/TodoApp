using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Services;

public interface IAuthService
{
    Task Register(RegisterViewModel request);

    Task<string> Login(LoginViewModel request);

}
