using FundooModel.Label;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.IRepository
{
    public interface ILabelRepository
    {
        public Task<int> AddLabel(Label label);
        public Label UpdateLabel(Label label);
        public IEnumerable<Label> GetAllLabels(int userId);
        public bool DeleteLabel(int LabelId);
    }
}