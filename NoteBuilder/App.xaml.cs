// Copyright (c) 2023 James Frost

// Licensed under the MIT License.
// See the LICENSE file in the project root for the full license text.

using NoteBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace NoteBuilder
{
    /// <summary>
    /// The main app class. It hosts the datamanager that contains the app's data.
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
