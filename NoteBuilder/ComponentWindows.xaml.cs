// Copyright (c) 2023 James Frost

// Licensed under the MIT License.
// See the LICENSE.txt file in the project root for the full license text.
using NoteBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
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
using System.Xml;

namespace NoteBuilder
{
    /// <summary>
    /// The Component window that edits the text blocks, creates new ones and deletes them.
    /// </summary>
    public partial class ComponentWindow : Window
    {

        private DataManager _dataManager;

        private static List<ComponentWindow> openWindows = new List<ComponentWindow>();
        public static bool IsAnyInstanceOpen => openWindows.Count > 0;

        private string type = "";
        private string action = "";

        private NoteBlock? noteBlock;
        public ComponentWindow(DataManager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;
            DataContext = _dataManager;

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
        #region New Block
        private void NewNoteBlock()
        {
            NewNoteBlockDialog dialog = new NewNoteBlockDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string newTitle = dialog.newTitle;
                noteBlock = new NoteBlock(newTitle, "", Guid.NewGuid(), false);
                action = "New";

            }
        }
        
        private void NewGreetingButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Greetings";
            NewNoteBlock();
        }

        private void NewRuleButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Rules";
            NewNoteBlock();
        }

        private void NewCitationButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Citations";
            NewNoteBlock();
        }

        private void NewSignoffButton_Click(object sender, RoutedEventArgs e)
        {
            type = "Signoffs";
            NewNoteBlock();
        }
        #endregion

        #region Load Block
        private void LoadGreetingButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = GreetingsComboBox.SelectedItem as NoteBlock;
            if (noteBlock.Placeholder == false)
            {
                noteBlockTextBox.Text = noteBlock!.Content;
                type = "Greetings";
                action = "Load";
            }
            else
            {
                noteBlock = null;
            }
            
        }

        private void LoadRuleButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = RulesComboBox.SelectedItem as NoteBlock;
            if (noteBlock.Placeholder == true)
            {
                noteBlockTextBox.Text = noteBlock!.Content;
                type = "Rules";
                action = "Load";
            }
            else
            {
                noteBlock = null;
            }
            
        }

        private void LoadCitationButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = CitationsComboBox.SelectedItem as NoteBlock;
            if (noteBlock.Placeholder == true)
            {
                noteBlockTextBox.Text = noteBlock!.Content;
                type = "Citations";
                action = "Load";
            }
            else
            {
                noteBlock = null;
            }
        }

        private void LoadSignoffButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = SignoffsComboBox.SelectedItem as NoteBlock;
            if (noteBlock.Placeholder == true)
            {
                noteBlockTextBox.Text = noteBlock!.Content;
                type = "Signoffs";
                action = "Load";
            }
            else
            {
                noteBlock= null;
            }
        }
        #endregion

        #region Delete Block
        private void DeleteBlock(NoteBlock block)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {block.Title}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _dataManager.DeleteBlock(type, block.Id);
            }
            type = "";
            noteBlock = null;
        }
        private void DeleteGreetingButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = GreetingsComboBox.SelectedItem as NoteBlock;
            type = "Greetings";
            DeleteBlock(noteBlock);
        }

        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = RulesComboBox.SelectedItem as NoteBlock;
            type = "Rules";
            DeleteBlock(noteBlock);
        }

        private void DeleteCitationButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = CitationsComboBox.SelectedItem as NoteBlock;
            type = "Citations";
            DeleteBlock(noteBlock);
        }

        private void DeleteSignoffButton_Click(object sender, RoutedEventArgs e)
        {
            noteBlock = SignoffsComboBox.SelectedItem as NoteBlock;
            type = "Signoffs";
            DeleteBlock(noteBlock);
        }
        #endregion
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (action != "" && type != "")
            {
                switch (action)
                {
                    case "New":
                        noteBlock.Content = noteBlockTextBox.Text;
                        _dataManager.AddBlock(type, noteBlock);
                        break;
                    case "Load":
                        NewNoteBlockDialog dialog = new NewNoteBlockDialog(noteBlock.Title);
                        bool? result = dialog.ShowDialog();
                        if (result == true)
                        {
                            string newTitle = dialog.newTitle;
                            noteBlock.Title = newTitle;
                        }
                        noteBlock.Content = noteBlockTextBox.Text;
                        noteBlock.Placeholder = false;
                        _dataManager.EditBlock(type, noteBlock);
                        break;
                    default:
                        break;
                }
                action = "";
                type = "";
                noteBlock = null;
                noteBlockTextBox.Text = string.Empty;
                ResetComboBoxes();
            }
        }

        private void ResetComboBoxes()
        {
            DataContext = null;
            DataContext = _dataManager;
            GreetingsComboBox.SelectedIndex = 0;
            RulesComboBox.SelectedIndex = 0;
            CitationsComboBox.SelectedIndex = 0;
            SignoffsComboBox.SelectedIndex = 0;
            ResetComboBoxesRequested?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ResetComboBoxesRequested;
    }
}
