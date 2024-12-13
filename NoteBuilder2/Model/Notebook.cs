using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBuilder2.Model
{
    public class Notebook
    {
        public ObservableCollection<Note> Notes { get; set; }
    }
}
