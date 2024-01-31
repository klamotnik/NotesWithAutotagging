namespace NotesWithAutotagging.Contracts.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
