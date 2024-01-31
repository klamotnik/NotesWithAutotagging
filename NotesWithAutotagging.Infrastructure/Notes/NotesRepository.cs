using Microsoft.AspNetCore.Http;
using NotesWithAutotagging.Contracts.Models;
using NotesWithAutotagging.Database;
using System.Security.Claims;

namespace NotesWithAutotagging.Infrastructure.Notes
{
    internal class NotesRepository : INotesRepository
    {
        private readonly NotesWithAutotaggingDbContext notesWithAutotaggingDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NotesRepository(NotesWithAutotaggingDbContext notesWithAutotaggingDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.notesWithAutotaggingDbContext = notesWithAutotaggingDbContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Note CreateNote(string note)
        {
            int userId;
            int.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue("id"), out userId);
            var user = notesWithAutotaggingDbContext.Users.First(p => p.Id == userId);
            AutoTagger autoTagger = new AutoTagger(note);
            var tags = autoTagger.TagNote();
            var noteDb = new Database.Models.Note
            {
                User = user,
                Content = note
            };
            var tagsDb = tags.Select(p => new Database.Models.Tag
            {
                Note = noteDb,
                TagName = p
            });
            noteDb.Tags = tagsDb.ToList();
            notesWithAutotaggingDbContext.Add(noteDb);
            notesWithAutotaggingDbContext.SaveChanges();
            return noteDb.ToContractNote();
        }

        public bool DeleteNote(int id)
        {
            int userId;
            int.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue("id"), out userId);
            var note = notesWithAutotaggingDbContext.Notes.FirstOrDefault(s => s.User.Id == userId && s.Id == id);
            if (note == null)
                return false;
            notesWithAutotaggingDbContext.Remove(note);
            notesWithAutotaggingDbContext.SaveChanges();
            return true;
        }

        public Note EditNote(int noteId, string note)
        {
            int userId;
            int.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue("id"), out userId);
            var noteDb = notesWithAutotaggingDbContext.Notes.FirstOrDefault(s => s.User.Id == userId && s.Id == noteId);
            if (noteDb == null)
                return null;
            noteDb.Content = note;
            notesWithAutotaggingDbContext.RemoveRange(noteDb.Tags);

            AutoTagger autoTagger = new AutoTagger(note);
            var tags = autoTagger.TagNote();
            var tagsDb = tags.Select(p => new Database.Models.Tag
            {
                Note = noteDb,
                TagName = p
            });
            
            notesWithAutotaggingDbContext.AddRange(tagsDb);
            notesWithAutotaggingDbContext.SaveChanges();
            return noteDb.ToContractNote();
        }

        public Note GetNote(int id)
        {
            int userId;
            int.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue("id"), out userId);
            var noteDb = notesWithAutotaggingDbContext.Notes.FirstOrDefault(s => s.User.Id == userId && s.Id == id);
            return noteDb?.ToContractNote();
        }

        public IEnumerable<Note> GetNotes()
        {
            int userId;
            int.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue("id"), out userId);
            var notesDb = notesWithAutotaggingDbContext.Notes.Where(s => s.User.Id == userId).ToList();
            return notesDb.Select(p => p.ToContractNote());
        }
    }
}
