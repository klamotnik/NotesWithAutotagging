using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Contracts.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public Note Note { get; set; }
        public string TagName { get; set; }
    }
}
