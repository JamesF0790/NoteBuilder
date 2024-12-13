using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder2.Model
{
    public class Note
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public NoteBlock Greeting { get; set; }
        public Rule Rule { get; set; }
        public NoteBlock Signoff { get; set; }
    }
}
