using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NotesWithAutotagging.Api;
using NotesWithAutotagging.Contracts.Models;
using NotesWithAutotagging.Database;

namespace NotesWithAutotagging.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/notes/")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;
        private readonly NotesWithAutotaggingDbContext dbContext;

        public NotesController(ILogger<NotesController> logger, NotesWithAutotaggingDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var id = User.Claims.First(p => p.Type == "id").Value;
            var notes = dbContext.Notes.Where(s => s.User.Id == int.Parse(id)).ToList();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            var userId = User.Claims.First(p => p.Type == "id").Value;
            var note = dbContext.Notes.FirstOrDefault(s => s.User.Id == int.Parse(userId) && s.Id == id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpPut]
        public IActionResult Create(string note)
        {
            var userId = User.Claims.First(p => p.Type == "id").Value;
            throw new Exception("Not exist! " + userId + " " + note);

        }

        [HttpPost("{id}")]
        public IActionResult Update(int id, string note)
        {
            var userId = User.Claims.First(p => p.Type == "id").Value;
            throw new Exception("Not exist! " + id + " " + note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = User.Claims.First(p => p.Type == "id").Value;
            var note = dbContext.Notes.FirstOrDefault(s => s.User.Id == int.Parse(userId) && s.Id == id);
            if (note == null)
                return NotFound();
            dbContext.Remove(note);
            return Ok();
        }
    }
}