using FundooModel.Entity;
using FundooModel.Notes;
using FundooRepository.Context;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        public const bool IS_CHECK = true;
        public readonly UserDbContext context;
        public NotesRepository(UserDbContext context)
        {
            this.context = context;
        }
        public Task<int> AddNotes(Note note)
        {
            this.context.Note.Add(note);
            var result = this.context.SaveChangesAsync();
            return result;
        }
        public Note EditNotes(Note note)
        {
            var data = this.context.Note.Where(x => x.Id == note.Id && x.Id == note.Id).FirstOrDefault();
            if(data != null)
            {
                data.Id = note.Id;
                data.Title = note.Title;
                data.Description = note.Description;
                data.Image = note.Image;
                data.Colour = note.Colour;
                data.Reminder = note.Reminder;
                data.IsArchive = note.IsArchive;
                data.IsPin = note.IsPin;
                data.IsTrash = note.IsTrash;
                data.CreatedDate = note.CreatedDate;
                data.ModifiedDate = note.ModifiedDate;
                this.context.Note.Update(data);
                this.context.SaveChangesAsync();
                return note;
            }
            return null;
        }
        public IEnumerable<Note> GetAllNotes(int userId)
        {
            var result = this.context.Note.Where(x => x.Id == userId).AsEnumerable();
            if (result != null)
            {
                return result;
            }
            return null;
        }
       
        public IEnumerable<Note> GetAllTrashNotes(int userId)
        {
            var result = this.context.Note.Where(x => x.Id == userId && x.IsTrash.Equals(IS_CHECK)).AsEnumerable();
            return result;
        }

        public IEnumerable<NotesCollab> GetAllNotesCollab(int userId)
        {
            List<NotesCollab> collab = new List<NotesCollab>();
            var result = this.context.Note.Join(this.context.Collaborator,
                Note => Note.Id,
                Collaborator => Collaborator.NoteId,
                (Note, Collaborator) => new NotesCollab
                {
                    NoteId = Note.NoteId,
                    Title = Note.Title,
                    Description= Note.Description,
                    Image=Note.Image,
                    Colour=Note.Colour,
                    Reminder=Note.Reminder,
                    IsArchive=Note.IsArchive,
                    IsPin=Note.IsPin,
                    IsTrash=Note.IsTrash,
                    CreatedDate=Note.CreatedDate,
                    ModifiedDate=Note.ModifiedDate,
                   });

            foreach(var data in result)
            {
                if(data.SenderUserId==userId|| data.ReceiverUserId == userId)
                {
                    collab.Add(data);
                }
               
            }
            return collab;
        }

        public bool DeleteNote(int noteId, int userId)
        {
            var result = this.context.Note.Where(x => x.Id == noteId && x.Id == userId).FirstOrDefault();
            if (result != null)
            {
                result.IsTrash = true;
                this.context.Note.Update(result);
                var deleteResult = this.context.SaveChanges();
                if (deleteResult == 1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool EmptyTrash(int userId)
        {
            var result = this.context.Note.Where(x => x.Id == userId && x.IsTrash.Equals(IS_CHECK)).ToList();
            foreach (var data in result)
            {
                this.context.Note.Remove(data);
            }
            var empty = this.context.SaveChanges();
            if (empty != 0)
            {
                return true;
            }
            return false;
        }
        public Note PinNote(int noteId, int userId)
        {
            var result = this.context.Note.Where(x => x.Id == noteId && x.Id == userId).FirstOrDefault();
            if (result != null)
            {
                result.IsPin = true;
                this.context.Note.Update(result);
                this.context.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public IEnumerable<Note> GetAllPinnedNotes(int userId)
        {
            var result = this.context.Note.Where(x => x.Id.Equals(userId) && x.IsPin.Equals(IS_CHECK)).AsEnumerable();
            return result;
        }
        public IEnumerable<Note> GetAllArchievedNotes(int userId)
        {
            var result = this.context.Note.Where(x => x.Id.Equals(userId) && x.IsArchive.Equals(IS_CHECK)).AsEnumerable();
            return result;
        }
        public Note ArchiveNotes(int noteId, int userId)
        {
            var result = this.context.Note.Where(x => x.Id == noteId && x.Id == userId).FirstOrDefault();
            if (result != null)
            {
                result.IsArchive = true;
                this.context.Note.Update(result);
                this.context.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public bool DeleteNotesForever(int noteId, int userId)
        {
            var result = this.context.Note.Where(x => x.Id == noteId && x.Id == userId).FirstOrDefault();
            if (result != null)
            {
                this.context.Note.Remove(result);
                var deleteResult = this.context.SaveChanges();
                if (deleteResult != 0)
                    return true;
            }
            return false;
        }
        public bool RestoreNotes(int noteId, int userId)
        {
            var result = this.context.Note.Where(x => x.Id == noteId && x.Id == userId).FirstOrDefault();
            if (result != null)
            {
                result.IsTrash = false;
                this.context.Note.Update(result);
                var restoreResult = this.context.SaveChanges();
                if (restoreResult != 0)
                    return true;
            }
            return false;
        }

        public Note AddNotesToFundoo(NotesEntity note, string emailId)
        {
            try
            {
                Note noteobj = new Note();
                var result = this.context.Note.Where(x => x.EmailId == emailId);
                if (result != null)
                {
                    noteobj.Id = note.Id;
                    noteobj.EmailId = emailId;
                    noteobj.Title = note.Title;
                    noteobj.Description = note.Description;
                    noteobj.Image = note.Image;
                    noteobj.Colour = note.Colour;
                    noteobj.Reminder = note.Reminder;
                    noteobj.IsArchive = note.IsArchive;
                    noteobj.IsPin = note.IsPin;
                    noteobj.IsTrash = note.IsTrash;
                    noteobj.CreatedDate = note.CreatedDate;
                    noteobj.ModifiedDate = note.ModifiedDate;
                    this.context.Note.Add(noteobj);
                    this.context.SaveChanges();
                }
                else
                {
                    return null;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}