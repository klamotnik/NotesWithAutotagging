using NotesWithAutotagging.Database.Models;
using NotesWithAutotagging.Infrastructure.Notes;

namespace NotesWithAutotagging.Tests.Infrastructure.Notes
{
    [TestFixture]
    public class NoteMapperTest
    {
        [Test]
        public void NoteMapper_FromDatabaseNote_ToContractsNote()
        {
            var noteDb = new Note
            {
                Id = 1,
                Content = "a@a.a",
                Tags = new List<Tag> 
                { 
                    new Tag 
                    { 
                        TagName = "EMAIL" 
                    } 
                }
            };

            var note = noteDb.ToContractNote();

            Assert.IsNotNull(note);
            Assert.AreEqual(noteDb.Id, note.Id);
            Assert.AreEqual(noteDb.Content, note.Content);
            Assert.AreEqual(noteDb.Tags.First().TagName, note.Tags.First());
        }
    }
}
