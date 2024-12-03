using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Contexts {
    public class TodoContext : DbContext {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
