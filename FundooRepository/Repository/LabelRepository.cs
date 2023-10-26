using FundooModel.Label;
using FundooModel.Notes;
using FundooRepository.Context;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        FundooNLog NLog = new FundooNLog();
        public readonly UserDbContext context;
        public LabelRepository(UserDbContext context)
        {
            this.context = context;
        }
        public Task<int> AddLabel(Label label)
        {
            try
            {
                this.context.labels.Add(label);
                var result = this.context.SaveChangesAsync();
                NLog.LogInfo("User Logged In");
                return result;
            }
            catch (Exception ex)
            {
                NLog.LogError("User not Logged In");
                return null;
            }
        }
        public Label UpdateLabel(Label label)
        {
            try
            {
                var result = this.context.labels.Where(x => x.LabelId == label.LabelId).FirstOrDefault();
                result.LabelName = label.LabelName;
                this.context.labels.Update(result);
                var data = this.context.SaveChanges();
                if (data != 0)
                {
                    NLog.LogInfo("User Logged In");
                    return result;
                }
                   
                NLog.LogError("User not Logged In");
                return null;
            }
            catch (Exception ex)
            {
                NLog.LogError("User not Logged In");
                return null;
            }
        }
        public IEnumerable<Label> GetAllLabels(int userId)
        {
            try
            {
                var result = this.context.labels.Where(x => x.Id.Equals(userId)).AsEnumerable();
                NLog.LogInfo("User Logged In");
                return result;
            }
            catch(Exception ex)
            {
                NLog.LogError("User not Logged In");
                return null;
            }
           
        }

        public IEnumerable<Label> GetAllLabelNotes(int userId)
        {
            try
            {
                List<Label> list = new List<Label>();
                var result = this.context.Note.Where(x => x.Id.Equals(userId)).Join(this.context.labels, Note => Note.Id, Label => Label.NoteId,
          (Note, Label) => new Label
          {
              NoteId = Note.Id,
              LabelId = Label.Id,
              LabelName = Label.LabelName,
          });
                foreach (var data in result)
                {
                    list.Add(data);
                }
                NLog.LogInfo("User Logged In");
                return list;
            }
            catch (Exception ex)
            {
                NLog.LogError("User not Logged In");
                return null;
            }

        }

        
        public bool DeleteLabel(int LabelId)
        {
            try
            {
                var result = this.context.labels.Where(x => x.LabelId == LabelId).FirstOrDefault();
                if (result != null)
                {

                    this.context.labels.Remove(result);
                    var deleteLabel = this.context.SaveChanges();
                    if (deleteLabel != 0)
                        NLog.LogInfo("User Logged In");
                        return true;
                }
                NLog.LogError("User not Logged In");
                return false;
            }
            catch (Exception ex)
            {
                NLog.LogError("User not Logged In");
                return false;
            }
        }
    }
}