using FundooModel.Entity;
using FundooModel.Notes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace FundooRepository.IRepository
{
    public interface INotesRepository
    {
        public Task<int> AddNotes(Note note);
        public Note ArchiveNotes(int noteId, int userId);
        public bool DeleteNote(int noteId, int userId);
        public Note EditNotes(Note note);
        public bool EmptyTrash(int userId);
        public IEnumerable<Note> GetAllArchievedNotes(int userId);
        public IEnumerable<Note> GetAllNotes(int userId);
        public IEnumerable<Note> GetAllPinnedNotes(int userId);
        public IEnumerable<Note> GetAllTrashNotes(int userId);
        public Note PinNote(int noteId, int userId);
        public bool DeleteNotesForever(int noteId, int userId);
        public bool RestoreNotes(int noteId, int userId);
        public Note AddNotesToFundoo(NotesEntity note, string emailId);

    }
}