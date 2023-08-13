using NoteBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ComponentWindow : Window
    {

        private DataManager _dataManager;

        private static List<ComponentWindow> openWindows = new List<ComponentWindow>();
        public static bool IsAnyInstanceOpen => openWindows.Count > 0;

        private string type = "";

        private NoteBlock? noteBlock;
        private bool newBlock = false;
        public ComponentWindow(DataManager dataManager)
        {
            InitializeComponent();
            DataContext = Application.Current;
            _dataManager = dataManager;

            openWindows.Add(this);
        }
        public static void FocusExistingWindow()
        {
            if (openWindows.Count > 0)
            {
                openWindows[0].Focus();
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            openWindows.Remove(this);
            base.OnClosed(e);
        }
        #region New NoteBlock
        private void NewNoteBlock()
        {
            NewNoteBlockDialog dialog = new NewNoteBlockDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string newTitle = dialog.newTitle;
                noteBlock = new NoteBlock(newTitle, "", Guid.NewGuid(), false);
                newBlock = true;

            }
        }
        
        private void NewGreetingButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Greeting";
            NewNoteBlock();
        }

        private void NewRuleButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Rule";
            NewNoteBlock();
        }

        private void NewCitationButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Citation";
            NewNoteBlock();
        }

        private void NewSignoffButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Signoff";
            NewNoteBlock();
        }
        #endregion

        #region Load Logic
        private void LoadGreetingButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = GreetingsComboBox.SelectedItem as NoteBlock;
            noteBlockTextBox.Text = noteBlock!.Content;
            type = "Greeting";
        }

        private void LoadRuleButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = RulesComboBox.SelectedItem as NoteBlock;
            noteBlockTextBox.Text = noteBlock!.Content;
            type = "Rule";
        }

        private void LoadCitationButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = CitationsComboBox.SelectedItem as NoteBlock;
            noteBlockTextBox.Text = noteBlock!.Content;
            type = "Citation";
        }

        private void LoadSignoffButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = SignoffsComboBox.SelectedItem as NoteBlock;
            noteBlockTextBox.Text = noteBlock!.Content;
            type = "Signoff";
        }
        #endregion

        #region Delete Block
        private void DeleteGreetingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCitationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSignoffButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case "Greeting":
                    if(noteBlock != null && noteBlockTextBox.Text != "")
                    {
                        noteBlock.Content = noteBlockTextBox.Text;
                    }
                    break;
                default:
                    break;
            }
        }
        
    }


}
