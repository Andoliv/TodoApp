using AutoMapper;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoViewModel>();
            //.ForMember(todo => todo.Item, options => options.MapFrom(prop => prop.Item + "(todo)"));
        CreateMap<TodoViewModel, Todo>();
    }
}
