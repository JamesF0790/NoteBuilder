using System.Windows;

namespace NoteBuilder
{
    /// <summary>
    /// Interaction logic for TagDialog.xaml
    /// </summary>
    public partial class TagDialog : Window
    {
        public string Answer { get; private set; } = string.Empty;
        public TagDialog(string prompt)
        {
            InitializeComponent();
            PromptTextBlock.Text = $"Insert text to replace {prompt}";
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Answer = AnswerTextBox.Text;
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
