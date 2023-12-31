﻿// Copyright (c) 2023 James Frost

// Licensed under the MIT License.
// See the LICENSE.txt file in the project root for the full license text.

using NoteBuilder.Model;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NoteBuilder
{
    /// <summary>
    /// The main window where the user selects the text blocks they want to use and generates the note.
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataManager _dataManager;

        private string greeting = "";
        private string rule = "";
        private string citation = "";
        private string signoff = "";


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainWindow()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public MainWindow(DataManager dataManager) : this()
        {
            InitializeComponent();
            _dataManager = dataManager;
            DataContext = _dataManager;  
        }

        protected override void OnClosed(EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }

            base.OnClosed(e);
        }

        private void CopyNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string note = NoteTextBox.Text;

            if(!string.IsNullOrEmpty(note))
            {
                Clipboard.SetText(note);

                var successAnimation = Resources["FlashSuccessAnimation"] as Storyboard;
                if(successAnimation != null)
                {
                    Storyboard.SetTarget(successAnimation, CopyNoteButton);
                    successAnimation.Begin();
                }
            }
            else
            {
                var errorAnimation = Resources["FlashErrorAnimation"] as Storyboard;
                if (errorAnimation != null)
                {
                    Storyboard.SetTarget(errorAnimation, CopyNoteButton);
                    errorAnimation.Begin();
                }
            }
        }

        private void GenerateNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RandomGreetingCheckBox.IsChecked == true)
            {
                Random random = new Random();
                int randomIndex = random.Next(_dataManager.GreetingsList.Count);
                greeting = _dataManager.GreetingsList[randomIndex].Content;
            }
            if (RandomSignoffButton.IsChecked == true)
            {
                Random random = new Random();
                int randomIndex = random.Next(_dataManager.SignoffsList.Count);
                signoff = _dataManager.SignoffsList[randomIndex].Content;
            }
            string pattern = @"<[^>]+>";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(rule);

            foreach (Match match in matches)
            {
                string placeholder = match.Value;
                string replacement = PromptForReplacement(placeholder);
                rule = rule.Replace(placeholder, replacement);
            }
            string generatedNote = $"{greeting}\n{rule}\n{citation}\n{signoff}";
            NoteTextBox.Text = generatedNote;
        }
        private string PromptForReplacement(string placeholder)
        {
            TagDialog dialog = new TagDialog(placeholder);
            if (dialog.ShowDialog() == true)
            {
                return dialog.Answer;
            }
            else 
            { 
                return placeholder;
            }
        }
        private void ComponentWindow_ResetComboBoxesRequested(object sender, EventArgs e)
        {
            ResetComboBoxes();
        }
        private void ModifyComponentsButton_Click(object sender, RoutedEventArgs e)
        {
            if(ComponentWindow.IsAnyInstanceOpen)
            {
                ComponentWindow.FocusExistingWindow();
            }
            else
            {
                ComponentWindow componentWindow = new ComponentWindow(_dataManager);
                componentWindow.ResetComboBoxesRequested += ComponentWindow_ResetComboBoxesRequested!;
                componentWindow.Show();
            }
        }

        private void GreetingsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GreetingsComboBox.SelectedItem is NoteBlock selectedNoteBlock)
            {
                greeting = selectedNoteBlock.Content;
            }
        }

        private void RulesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RulesComboBox.SelectedItem is NoteBlock selectedRuleBlock)
            {
                rule = selectedRuleBlock.Content;
            }
        }

        private void CitationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CitationsComboBox.SelectedItem is NoteBlock selectedCitationBlock)
            {
                citation = selectedCitationBlock.Content;
            }    
        }

        private void SignoffsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SignoffsComboBox.SelectedItem is NoteBlock selectedSignoffBlock)
            {
                signoff = selectedSignoffBlock.Content;
            }
        }
        public void ResetComboBoxes()
        {
            DataContext = null;
            DataContext = _dataManager;
            GreetingsComboBox.SelectedIndex = 0;
            RulesComboBox.SelectedIndex = 0;
            CitationsComboBox.SelectedIndex = 0;
            SignoffsComboBox.SelectedIndex = 0;
        }

        private void RandomGreetingCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            GreetingsComboBox.IsEnabled = false;
        }
        private void RandomGreetingCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            GreetingsComboBox.IsEnabled=true;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            NoteBlock tempBlock = GreetingsComboBox.SelectedItem as NoteBlock;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            greeting = tempBlock!.Content;
        }
        private void RandomSignoffButton_Checked(object sender, RoutedEventArgs e)
        {
            SignoffsComboBox.IsEnabled = false;
        }
        private void RandomSignoffButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SignoffsComboBox.IsEnabled=true;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            NoteBlock tempBlock = SignoffsComboBox.SelectedItem as NoteBlock;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            signoff = tempBlock!.Content;

        }
    }
}
