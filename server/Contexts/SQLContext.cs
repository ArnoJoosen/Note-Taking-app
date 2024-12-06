using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Contexts {
    public class SQLContext : DbContext {
        public SQLContext(DbContextOptions<SQLContext> options) : base(options) {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
