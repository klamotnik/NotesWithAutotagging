namespace NotesWithAutotagging.Infrastructure.Notes
{
    public static class NoteMapper
    {
        public static Contracts.Models.Note ToContractNote(this Database.Models.Note note)
        {
            return new Contracts.Models.Note
            {
                Id = note.Id,
                Content = note.Content,
                Tags = note.Tags.Select(p => p.TagName)
            };
        }
    }
}
