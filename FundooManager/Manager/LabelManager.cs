using FundooManager.IManager;
using FundooModel.Label;
using FundooModel.Notes;
using FundooRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        public readonly ILabelRepository labelRepository;
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public Task<int> AddLabel(Label label)
        {
            var result = this.labelRepository.AddLabel(label);
            return result;
        }
        public Label UpdateLabel(Label label)
        {
            var result = this.labelRepository.UpdateLabel(label);
            return result;
        }
        public IEnumerable<Label> GetAllLabels(int userId)
        {
            var result = this.labelRepository.GetAllLabels(userId);
            return result;
        }

        public IEnumerable<Label> GetAllLabelNotes(int userId)
        {
            var result = this.labelRepository.GetAllLabelNotes(userId);
            return result;
        }
        public bool DeleteLabel(int LabelId)
        {
            var result = this.labelRepository.DeleteLabel(LabelId);
            return result;
        }
    }
}