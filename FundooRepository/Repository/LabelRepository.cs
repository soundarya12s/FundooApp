using FundooModel.Label;
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
    public class LabelRepository : ILabelRepository
    {
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
                return result;
            }
            catch (Exception ex)
            {
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
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IEnumerable<Label> GetAllLabels(int userId)
        {
            try
            {
                var result = this.context.labels.Where(x => x.Id == userId).AsEnumerable();
                if (result != null)
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Label> GetAllLabelNotes(int userId, int labelId)
        {
            try
            {
                var result = this.context.labels.Where(x => x.Id == userId && x.LabelId == labelId).AsEnumerable();
                if (result != null)
                    return result;
                return null;
            }
            catch (Exception ex)
            {
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
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}