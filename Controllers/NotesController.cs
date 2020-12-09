using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Core;
using Notes.DB;

namespace MyCommunityLandmark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Bind("Id,Username,Value")]

    public class NotesController : ControllerBase
    {

        private readonly ILogger<NotesController> _logger;
        private INotesServices _notesSerives;
        private readonly AppDbContext _context;

        public NotesController(ILogger<NotesController> logger, INotesServices notesSerives, AppDbContext context)
        {
            _logger = logger;
            _notesSerives = notesSerives;
            _context = context;
        }



        [HttpGet("{id}", Name ="GetNote") ]
        public IActionResult GetNote(int id)
        {
            return Ok(_notesSerives.GetNote(id));
        }

        [HttpGet("", Name = "GetAllNote")]
        public IActionResult GetAllNote()
        {
            return Ok(_notesSerives.GetAllNote());
        }

        [HttpGet("GetNoteByUsername/{username}")]
        public IActionResult GetNoteByName(string username)
        {
            //_notesSerives objUser = new _notesSerives();
            //int ID = Convert.ToInt32(userId);
            if (_notesSerives.GetNoteByName(username) == null)
            {
                return NotFound();

            }

            return Ok(_notesSerives.GetNoteByName(username));

        }

        [HttpPost]
        public IActionResult CreateNote(Note note)
        {
            var newNote = _notesSerives.CreateNote(note);
            return CreatedAtRoute("GetNote", new { newNote.Id}, newNote);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            _notesSerives.DeleteNote(id);
               
            return Ok();
        }

        [HttpPut]

        public IActionResult UpdateNote(Note note)
        {
            _notesSerives.UpdateNote(note);

            return Ok(_notesSerives.UpdateNote(note));

        }
    }
}

