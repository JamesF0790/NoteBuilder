using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoteBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string dataFolderPath = "Data";

            CreateMissingJsonFile(dataFolderPath, "Greetings.json");
            CreateMissingJsonFile(dataFolderPath, "Rules.json");
            CreateMissingJsonFile(dataFolderPath, "Citations.json");
            CreateMissingJsonFile(dataFolderPath, "Signoffs.json");
        }

        private void CreateMissingJsonFile(string folderPath, string fileName)
        {
            string filePath = Path.Combine(folderPath, fileName);

            if(!File.Exists(filePath))
            {
                Directory.CreateDirectory(folderPath);
                File.WriteAllText(filePath, "[]");
            }
        }
    }
}
