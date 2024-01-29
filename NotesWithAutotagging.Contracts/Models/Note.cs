using NotesWithAutotagging.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Contracts.Notes
{
    public class Note
    {
        public string Content { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
