using FundooModel.Notes;
using FundooRepository.Context;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        public readonly UserDbContext context;
        public CollaboratorRepository(UserDbContext context)
        {
            this.context = context;
        }

        public Task<int> AddCollaborator(Collaborator collaborator)
        {
            this.context.Collaborator.Add(collaborator);
            var result = this.context.SaveChangesAsync();
            return result;
        }

        public bool DeleteCollab(int noteId, int userId)
        {
            var result = this.context.Collaborator.Where(x => x.NoteId == noteId && x.ReceiverUserId == userId).FirstOrDefault();
            if (result != null)
            {

                this.context.Collaborator.Remove(result);
                var deleteResult = this.context.SaveChanges();
                if (deleteResult == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Collaborator> GetAllCollabNotes(int userId, string labelId)
        {
            var result = this.context.Collaborator.Where(x => x.SenderUserId == userId || x.ReceiverUserId == userId).AsEnumerable();
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }

   
}
