using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesWithAutotagging.Contracts.Notes;

namespace NotesWithAutotagging.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/notes/")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IEnumerable<Note>> GetList()
        {
            throw new Exception("Not exist!");
        }

        [HttpGet("{id}")]
        public async Task<Note> Get(int id)
        {
            throw new Exception("Not exist! " + id);
        }

        [HttpPut]
        public async Task Create(string note)
        {
            throw new Exception("Not exist! " + note);
        }

        [HttpPost("{id}")]
        public async Task Update(int id, string note)
        {
            throw new Exception("Not exist! " + id + " " + note);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            throw new Exception("Not exist! " + id);
        }
    }
}