using NoteBuilder2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder2.Services
{
    public class DataService
    {
        public ObservableCollection<NoteBlock> Greetings { get; set; }
        public ObservableCollection<NoteBlock> GlobalCitations { get; set; }
        public ObservableCollection<Rule> Rules { get; set; }

        public ObservableCollection<NoteBlock> Signoffs { get; set; }
    }
}
