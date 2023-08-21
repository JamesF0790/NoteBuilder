// Copyright (c) 2023 James Frost

// Licensed under the MIT License.
// See the LICENSE.txt file in the project root for the full license text.
using System.Windows;

namespace NoteBuilder
{
    /// <summary>
    /// Pop up dialog that lets the user enter a title for a text block.
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
