using NotesWithAutotagging.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Infrastructure.Notes
{
    public interface INotesRepository
    {
        public IEnumerable<Note> GetNotes();
        public Note GetNote(int id);
        public Note CreateNote(string note);
        public Note EditNote(int noteId, string note);
        public bool DeleteNote(int id);
    }
}
