using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder2.Model
{
    public class Rule
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public Uri Url { get; set; }
        public ObservableCollection<NoteBlock> Violations { get; set; } = new ObservableCollection<NoteBlock>();
        public ObservableCollection<NoteBlock> Citations { get; set; } = new ObservableCollection<NoteBlock>();

        public override string ToString()
        {
            return Name;
        }
    }
}
