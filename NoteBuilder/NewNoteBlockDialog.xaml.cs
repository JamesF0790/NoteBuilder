using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteBuilder
{
    /// <summary>
    /// Interaction logic for NewNoteBlockDialogue.xaml
    /// </summary>
    public partial class NewNoteBlockDialog : Window
    {
        public string newTitle { get; private set; }
        public NewNoteBlockDialog(string initialTitle = "")
        {
            InitializeComponent();
            TitleTextBox.Text = initialTitle;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            newTitle = TitleTextBox.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
