using FundooManager.IManager;
using FundooModel.Entity;
using FundooModel.Notes;
using FundooRepository.IRepository;
//using FundooRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace FundooManager.Manager
{
    public class NotesManager : INotesManager
    {
        public readonly INotesRepository notesRepository;
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }
        public Task<int> AddNotes(Note note)
        {
            var result = this.notesRepository.AddNotes(note);
            return result;
        }
        public Note ArchiveNotes(int noteId, int userId)
        {
            var result = this.notesRepository.ArchiveNotes(noteId, userId);
            return result;
        }
        public bool DeleteNote(int noteId, int userId)
        {
            var result = this.notesRepository.DeleteNote(noteId, userId);
            return result;
        }
        public Note EditNotes(Note note)
        {
            var result = this.notesRepository.EditNotes(note);
            return result;
        }
        public bool EmptyTrash(int userId)
        {
            var result = this.notesRepository.EmptyTrash(userId);
            return result;
        }
        public IEnumerable<Note> GetAllArchievedNotes(int userId)
        {
            var result = this.notesRepository.GetAllArchievedNotes(userId);
            return result;
        }
        public IEnumerable<Note> GetAllNotes(int userId)
        {
            var result = this.notesRepository.GetAllNotes(userId);
            return result;
        }
        public IEnumerable<Note> GetAllPinnedNotes(int userId)
        {
            var result = this.notesRepository.GetAllPinnedNotes(userId);
            return result;
        }
        public IEnumerable<Note> GetAllTrashNotes(int userId)
        {
            var result = this.notesRepository.GetAllTrashNotes(userId);
            return result;
        }
        public Note PinNote(int noteId, int userId)
        {
            var result = this.notesRepository.PinNote(noteId, userId);
            return result;
        }

        public bool DeleteNotesForever(int noteId, int userId)
        {
            var result = this.notesRepository.DeleteNotesForever(noteId, userId);
            return result;
        }
        public bool RestoreNotes(int noteId, int userId)
        {
            var result = this.notesRepository.RestoreNotes(noteId, userId);
            return result;
        }
        public Note AddNotesToFundoo(NotesEntity note, string emailId)
        {
            var result = this.notesRepository.AddNotesToFundoo(note, emailId);
            return result;
        }
    }
}