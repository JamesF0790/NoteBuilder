﻿using System;
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