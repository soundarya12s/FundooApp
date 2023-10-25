using FundooModel.Label;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.IManager
{
    public interface ILabelManager
    {
        public Task<int> AddLabel(Label label);
        public Label UpdateLabel(Label label);
        public IEnumerable<Label> GetAllLabels(int userId);
        public bool DeleteLabel(int LabelId);
    }
}