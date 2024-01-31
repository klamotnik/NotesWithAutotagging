using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Database.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}
