using FundooManager.IManager;
using FundooModel.Notes;
using FundooModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public readonly INotesManager notesManager;
        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }
        [HttpPost]
        [Route("AddNote")]
        public async Task<ActionResult> AddNote(Note note)
        {
            try
            {
                var result = await this.notesManager.AddNotes(note);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Note Added Successfully", Data = note });
                }
                return this.BadRequest(new { Status = false, Message = "Adding note Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("ArchieveNote")]
        public ActionResult ArchiveNotes(int noteId, int userId)
        {
            try
            {
                var result = this.notesManager.ArchiveNotes(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Archieved Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Archieving Note Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteNote")]
        public ActionResult DeleteNote(int noteId, int userId)
        {
            try
            {
                var result = this.notesManager.DeleteNote(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Note Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("EditNote")]
        public ActionResult EditNote(Note note)
        {
            try
            {
                var result = this.notesManager.EditNotes(note);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Edited Successfully", Data = note });
                }
                return this.BadRequest(new { Status = false, Message = "Editing Note Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("EmptyTrash")]
        public ActionResult EmptyTrash(int userId)
        {
            try
            {
                var result = this.notesManager.EmptyTrash(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Trash Emptied" });
                }
                return this.BadRequest(new { Status = false, Message = "Empting Trash Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllNotes")]
        public async Task<ActionResult> GetAllNotes(int userId)
        {
            try
            {
                var result = this.notesManager.GetAllNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Note Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Note Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllArchievedNotes")]
        public async Task<ActionResult> GetAllArchievedNotes(int userId)
        {
            try
            {
                var result = this.notesManager.GetAllArchievedNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Archieved Note Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Archieved Note Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllPinnedNotes")]
        public async Task<ActionResult> GetAllPinnedNotes(int userId)
        {
            try
            {
                var result = this.notesManager.GetAllPinnedNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Pinned Note Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Pinned Note Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllTrashNotes")]
        public async Task<ActionResult> GetAllTrashNotes(int userId)
        {
            try
            {
                var result = this.notesManager.GetAllTrashNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Trashed Note Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Trashed Note Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("PinNote")]
        public ActionResult PinNote(int noteId, int userId)
        {
            try
            {
                var result = this.notesManager.PinNote(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Pinned Successful", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Pinning Note Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }


        [HttpPut]
        [Route("DeleteNotesForever")]
        public ActionResult DeleteNotesForever(int noteId, int userId)
        {
            try
            {
                var result = this.notesManager.DeleteNotesForever(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Note Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("RestoreNotes")]
        public ActionResult RestoreNotes(int noteId, int userId)
        {
            try
            {
                var result = this.notesManager.RestoreNotes(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note Restored Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Restoring Note Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        public static string GetUserEmail(HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}