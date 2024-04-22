using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Models;
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(prop => prop.Item)
            .HasMaxLength(256);

        base.OnModelCreating(modelBuilder);
    }

}