namespace NotesWithAutotagging.Infrastructure.Models
{
    public enum TagDb { Phone = 1, Email = 2 }
    public class NoteDb
    {
        public int Id { get; set; }
        public int User { get; set; }
        public string Note { get; set; }
        public IEnumerable<TagDb> Tags { get; set; }
    }
}