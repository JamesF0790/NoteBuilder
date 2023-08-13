using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder.Model
{

    public class NoteBlockCollection
    {
        public List<NoteBlock> NoteBlocks { get; set; } = new List<NoteBlock>();
    }
    public class NoteBlock
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid Id { get; set; }
        public bool Placeholder { get; set; }

        public NoteBlock(string title, string content, Guid id, bool placeholder)
        {
            Title = title;
            Content = content;
            Id = id;
            Placeholder = placeholder;
        }
    }
}
