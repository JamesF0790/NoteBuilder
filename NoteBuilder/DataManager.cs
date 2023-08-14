using NoteBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.IO.Packaging;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.ComponentModel;

namespace NoteBuilder
{
    public class DataManager: INotifyPropertyChanged
    {
        private readonly string dataFolderPath = "Data";

        private List<NoteBlock> _greetingsList;
        private List<NoteBlock> _rulesList;
        private List<NoteBlock> _citationsList;
        private List<NoteBlock> _signoffsList;

        public List<NoteBlock> GreetingsList
        {
            get => _greetingsList.OrderBy(noteBlock => noteBlock.Title).ToList();
            set
            {
                _greetingsList = value;
                OnPropertyChanged(nameof(GreetingsList));
            }
        }
        public List<NoteBlock> RulesList
        {
            get =>_rulesList.OrderBy(noteBlock => noteBlock.Title).ToList();
            set
            {
                _rulesList = value;
                OnPropertyChanged(nameof(RulesList));
            }
        }
        public List<NoteBlock> CitationsList
        {
            get => _citationsList.OrderBy(noteBlock => noteBlock.Title).ToList();
            set
            {
                _citationsList = value;
                OnPropertyChanged(nameof(CitationsList));
            }
        }
        public List<NoteBlock> SignoffsList
        {
            get => _signoffsList.OrderBy(noteBlock => noteBlock.Title).ToList();
            set
            {
                _signoffsList = value;
                OnPropertyChanged(nameof(SignoffsList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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


        private void SaveNoteBlockCollectionToJson(NoteBlockCollection collection, string fileName)
        {
            string filePath = Path.Combine(dataFolderPath, fileName);
            string collectionJson = JsonSerializer.Serialize(collection, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, collectionJson);
        }
        private void SaveListToJson(List<NoteBlock> list, string fileName)
        {
            NoteBlockCollection collection = new NoteBlockCollection { NoteBlocks = list};
            SaveNoteBlockCollectionToJson(collection, fileName);
        }
        private List<NoteBlock> LoadNoteBlocksFromJson(string fileName)
        {
            string filePath = Path.Combine(dataFolderPath, fileName);
            string json = File.ReadAllText(filePath);
            NoteBlockCollection collection = JsonSerializer.Deserialize<NoteBlockCollection>(json)!;
            return collection.NoteBlocks;
        }
        
        public void AddBlock(string type, NoteBlock noteBlock)
        {
            switch (type)
            {
                case "Greetings":
                    NoteBlock placeholderGreetingsBlock = _greetingsList.FirstOrDefault(block => block.Placeholder);
                    if (placeholderGreetingsBlock != null)
                    {
                        _greetingsList.Remove(placeholderGreetingsBlock);
                    }
                    _greetingsList.Add(noteBlock);
                    SaveListToJson(_greetingsList, "Greetings.json");
                    break;
                case "Rules":
                    NoteBlock placeholderRulesBlock = _rulesList.FirstOrDefault(block => block.Placeholder);
                    if (placeholderRulesBlock != null)
                    {
                        _rulesList.Remove(placeholderRulesBlock);
                    }
                    _rulesList.Add(noteBlock);
                    SaveListToJson(_rulesList, "Rules.json");
                    break;
                case "Citations":
                    NoteBlock placeholderCitationsBlock = _citationsList.FirstOrDefault(block => block.Placeholder);
                    if(placeholderCitationsBlock != null)
                    {
                        _citationsList.Remove(placeholderCitationsBlock);
                    }
                    _citationsList.Add(noteBlock);
                    SaveListToJson(_citationsList, "Citations.json");
                    break;
                case "Signoffs":
                    NoteBlock placeholderSignoffsBlock = _signoffsList.FirstOrDefault(block =>  block.Placeholder);
                    if (placeholderSignoffsBlock != null)
                    {
                        _signoffsList.Remove(placeholderSignoffsBlock);
                    }
                    _signoffsList.Add(noteBlock);
                    SaveListToJson(_signoffsList, "Signoffs.json");
                    break;
                default:
                    throw new ArgumentException("Invalid Type");
            }
        }

        public void EditBlock(string type, NoteBlock editedBlock)
        {
            switch (type)
            {
                case "Greetings":
                    int greetingsIndex = _greetingsList.FindIndex(block => block.Id == editedBlock.Id);
                    if (greetingsIndex != -1)
                    {
                        _greetingsList[greetingsIndex] = editedBlock;
                    }
                    SaveListToJson(_greetingsList, "Greetings.json");
                    break;
                case "Rules":
                    int rulesIndex = _rulesList.FindIndex(block => block.Id == editedBlock.Id);
                    if (rulesIndex != -1)
                    {
                        _rulesList[rulesIndex] = editedBlock;
                    }
                    SaveListToJson(_rulesList, "Rules.json");
                    break;
                case "Citations":
                    int citationsIndex = _citationsList.FindIndex(block => block.Id == editedBlock.Id);
                    if (citationsIndex != -1)
                    {
                        _citationsList[citationsIndex] = editedBlock;
                    }
                    SaveListToJson(_citationsList, "Citations.json");
                    break;
                case "Signoffs":
                    int signoffsIndex = _signoffsList.FindIndex(block => block.Id == editedBlock.Id);
                    if (signoffsIndex != -1)
                    {
                        _signoffsList[signoffsIndex] = editedBlock;
                    }
                    SaveListToJson(_signoffsList, "Signoffs.json");
                    break;
                default:
                    throw new ArgumentException("Invalid Type");
            }
        }
        public void DeleteBlock(string type, Guid blockID)
        {
            NoteBlock placeholder = new NoteBlock("Placeholder Block", "Don't use me", Guid.NewGuid(), true);
            switch (type)
            {
                case "Greetings":
                    NoteBlock greetingsBlock = _greetingsList.FirstOrDefault(block => block.Id == blockID);
                    if (greetingsBlock != null)
                    {
                        _greetingsList.Remove(greetingsBlock);
                    }
                    if (_greetingsList.Count == 0)
                    {
                        _greetingsList.Add(placeholder);
                    }
                    SaveListToJson(_greetingsList, "Greetings.json");
                    break;
                case "Rules":
                    NoteBlock rulesBlock = _rulesList.FirstOrDefault(block => block.Id == blockID);
                    if (rulesBlock != null)
                    {
                        _rulesList.Remove(rulesBlock);
                    }
                    if (_rulesList.Count == 0)
                    {
                        _rulesList.Add(placeholder);
                    }
                    SaveListToJson(_rulesList, "Rules.json");
                    break;
                case "Citations":
                    NoteBlock citationsBlock = _citationsList.FirstOrDefault(block => block.Id == blockID);
                    if (citationsBlock != null)
                    {
                        _citationsList.Remove(citationsBlock);
                    }
                    if (_citationsList.Count == 0)
                    {
                        _citationsList.Add(placeholder);
                    }
                    SaveListToJson(_citationsList, "Citations.json");
                    break;
                case "Signoffs":
                    NoteBlock signoffsBlock = _signoffsList.FirstOrDefault(block => block.Id == blockID);
                    if (signoffsBlock != null)
                    {
                        _signoffsList.Remove(signoffsBlock);
                    }
                    if (_signoffsList.Count == 0)
                    {
                        _signoffsList.Add(placeholder);
                    }
                    SaveListToJson(_signoffsList, "Signoffs.json");
                    break;
                default:
                    throw new ArgumentException("Invalid Type");
            }
        }
    }
}
