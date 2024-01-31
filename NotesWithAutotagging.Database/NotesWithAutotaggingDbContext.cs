using Microsoft.EntityFrameworkCore;
using NotesWithAutotagging.Database.Models;

namespace NotesWithAutotagging.Database
{
    public class NotesWithAutotaggingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public NotesWithAutotaggingDbContext(DbContextOptions<NotesWithAutotaggingDbContext> options) : base(options)
        {
        }
    }
}