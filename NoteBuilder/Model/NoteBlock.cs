using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder.Model
{
    public class NoteBlock
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid Id { get; set; }

        public NoteBlock(string title, string content, Guid id)
        {
            Title = title;
            Content = content;
            Id = id;
        }
    }
}
