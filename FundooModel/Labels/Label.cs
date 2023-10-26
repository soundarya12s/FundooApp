using FundooModel.Notes;
using FundooModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModel.Label
{
    public class Label
    {
       public int LabelId { get; set; }
        public string LabelName { get; set; }
        public int Id { get; set; }
        [ForeignKey("Id")]
        public Register Register { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("Note")]
        public Note Note { get; set; }
        
    }
}
