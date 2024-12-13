using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder2.Model
{
    public class NoteBlock
    {
        public String Title { get; set; }
        public NoteBlockType Type { get; set; }
        public String Content { get; set; }
        public Guid Id { get; set; }

    }
}
