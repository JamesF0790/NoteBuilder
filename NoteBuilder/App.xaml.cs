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

        public List<NoteBlock> GreetingsList { get; private set; }
        public List<NoteBlock> RulesList { get; private set; }
        public List<NoteBlock> CitationsList { get; private set; }
        public List<NoteBlock> SignoffsList { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _dataManager = new DataManager();
            GreetingsList = _dataManager.GreetingsList;
            RulesList = _dataManager.RulesList;
            CitationsList = _dataManager.CitationsList;
            SignoffsList = _dataManager.SignoffsList;

            MainWindow mainWindow = new MainWindow(_dataManager);
            mainWindow.Show();

        }
    }
}
