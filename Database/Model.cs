using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DiaryBackend {
    public class BackendContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder) {
            OptionsBuilder.UseSqlite("Data Source=backend.db");
        }
    }
    public class User {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Entry> Entries { get; set; }
    }
    public class Entry {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        public bool Deleted { get; set; }
        public string Locked { get; set; }
    }

}