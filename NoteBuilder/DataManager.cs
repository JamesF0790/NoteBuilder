using NoteBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.IO.Packaging;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace NoteBuilder
{
    public class DataManager
    {
        private readonly string dataFolderPath = "Data";

        public List<NoteBlock> GreetingsList { get; private set; }
        public List<NoteBlock> RulesList { get; private set; }
        public List<NoteBlock> CitationsList { get; private set; }
        public List<NoteBlock> SignoffsList { get; private set; }

        public DataManager()
        {
            EnsureDirectoryExists(dataFolderPath);
            EnsureJsonFilesExistAndLoadData();
        }
        public void EnsureJsonFilesExistAndLoadData()
        {
            EnsureJsonFilesAndGetNoteBlocks("Greetings.json");
            EnsureJsonFilesAndGetNoteBlocks("Rules.json");
            EnsureJsonFilesAndGetNoteBlocks("Citations.json");
            EnsureJsonFilesAndGetNoteBlocks("Signoffs.json");

            GreetingsList = LoadNoteBlocksFromJson("Greetings.json");
            RulesList = LoadNoteBlocksFromJson("Rules.json");
            CitationsList = LoadNoteBlocksFromJson("Citations.json");
            SignoffsList = LoadNoteBlocksFromJson("Signoffs.json");
        }
        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void EnsureJsonFilesAndGetNoteBlocks(string fileName)
        {
            string filePath = Path.Combine(dataFolderPath, fileName);

            if(!File.Exists(filePath))
            {
                NoteBlockCollection defaultCollection = CreateDefaultNoteBlockCollection();
                SaveNoteBlockCollectionToJson(defaultCollection, fileName);
            }
        }

        private NoteBlockCollection CreateDefaultNoteBlockCollection()
        {
            NoteBlock defaultNoteBlock = new NoteBlock("Add new data", "Example data block", Guid.NewGuid(),true);
            NoteBlockCollection defaultCollection = new NoteBlockCollection
            {
                NoteBlocks = new List<NoteBlock> { defaultNoteBlock }
            };
            return defaultCollection;
        }


        public void SaveNoteBlockCollectionToJson(NoteBlockCollection collection, string fileName)
        {
            string filePath = Path.Combine(dataFolderPath, fileName);
            string collectionJson = JsonSerializer.Serialize(collection, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, collectionJson);
        }

        private List<NoteBlock> LoadNoteBlocksFromJson(string fileName)
        {
            string filePath = Path.Combine(dataFolderPath, fileName);
            string json = File.ReadAllText(filePath);
            NoteBlockCollection collection = JsonSerializer.Deserialize<NoteBlockCollection>(json)!;
            return collection.NoteBlocks;
        }
    }
}
