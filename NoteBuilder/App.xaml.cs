using NoteBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace NoteBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private DataManager _dataManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _dataManager = new DataManager();

            MainWindow mainWindow = new MainWindow(_dataManager);
            mainWindow.Show();

        }
    }
}
