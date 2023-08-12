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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
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

        }

        private void ModifyComponentsButton_Click(object sender, RoutedEventArgs e)
        {
            ComponentWindow componentWindow = new ComponentWindow();
            componentWindow.Show();
        }
    }
}
