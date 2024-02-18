using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // When deleting a person, un-assign him/her from existing todo items.
            builder.Entity<Person>()
                .HasMany(person => person.AssignedTodoItems)
                .WithOne(todoItem => todoItem.Assignee)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // When deleting a person, the todo items he/she owns are deleted too.
            builder.Entity<TodoItem>()
                .HasOne(todoItem => todoItem.Owner)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
