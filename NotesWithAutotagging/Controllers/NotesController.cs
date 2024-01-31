using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NotesWithAutotagging.Api;
using NotesWithAutotagging.Infrastructure.Notes;

namespace NotesWithAutotagging.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/notes/")]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var notes = notesRepository.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            var note = notesRepository.GetNote(id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpPut]
        public IActionResult Create(string note)
        {
            var createdNote = notesRepository.CreateNote(note);
            return Ok(createdNote);
        }

        [HttpPost("{id}")]
        public IActionResult Update(int id, string note)
        {
            var editedNote = notesRepository.EditNote(id, note);
            if (editedNote == null)
                return NotFound();
            return Ok(editedNote);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = notesRepository.DeleteNote(id);
            if(!result)
                return NotFound();
            return Ok();
        }
    }
}