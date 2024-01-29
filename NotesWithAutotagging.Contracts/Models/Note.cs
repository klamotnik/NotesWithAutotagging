﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Contracts.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
