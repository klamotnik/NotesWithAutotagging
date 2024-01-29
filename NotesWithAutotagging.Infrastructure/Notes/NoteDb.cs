namespace NotesWithAutotagging.Infrastructure.Models
{
    public enum Tag { Phone = 1, Email = 2 }
    public class NoteDb
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}