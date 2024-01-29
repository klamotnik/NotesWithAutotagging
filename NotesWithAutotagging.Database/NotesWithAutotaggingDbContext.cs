using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotesWithAutotagging.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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