using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesWithAutotagging.Contracts.Models;
using NotesWithAutotagging.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Infrastructure.Notes
{
    internal class NotesRepository : INotesRepository
    {
        private readonly NotesWithAutotaggingDbContext notesWithAutotaggingDbContext;

        public NotesRepository(NotesWithAutotaggingDbContext notesWithAutotaggingDbContext)
        {
            this.notesWithAutotaggingDbContext = notesWithAutotaggingDbContext;
        }
        public Note CreateNote(string note)
        {
            var user = notesWithAutotaggingDbContext.Users.First(p => p.Id == 1);
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
            var note = notesWithAutotaggingDbContext.Notes.FirstOrDefault(s => s.User.Id == 1 && s.Id == id);
            if (note == null)
                return false;
            notesWithAutotaggingDbContext.Remove(note);
            notesWithAutotaggingDbContext.SaveChanges();
            return true;
        }

        public Note EditNote(int noteId, string note)
        {
            var noteDb = notesWithAutotaggingDbContext.Notes.FirstOrDefault(s => s.User.Id == 1 && s.Id == noteId);
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
            var noteDb = notesWithAutotaggingDbContext.Notes.FirstOrDefault(s => s.User.Id == 1 && s.Id == id);
            return noteDb?.ToContractNote();
        }

        public IEnumerable<Note> GetNotes()
        {
            var notesDb = notesWithAutotaggingDbContext.Notes.Where(s => s.User.Id == 1).ToList();
            return notesDb.Select(p => p.ToContractNote());
        }
    }
}
