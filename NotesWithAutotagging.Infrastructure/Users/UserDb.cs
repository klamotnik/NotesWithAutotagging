using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWithAutotagging.Infrastructure.Users
{
    public class UserDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Password { get; set; }
    }
}
