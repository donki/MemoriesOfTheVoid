using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MemoriesOfTheVoid.Systems
{
    public class SaveData
    {
        public Vector2 PlayerPosition { get; set; }
        public List<string> InventoryItems { get; set; } = new List<string>();
        public Dictionary<string, bool> GameFlags { get; set; } = new Dictionary<string, bool>();
        public string CurrentScene { get; set; }
        public string CurrentLanguage { get; set; } = "es";
        public DateTime SaveTime { get; set; } = DateTime.Now;
        public int ChapterProgress { get; set; } = 1;
    }

    public class SaveSystem
    {
        private const string SAVE_FILE = "savegame.json";
        private const string SAVE_DIRECTORY = "Saves";

        public SaveSystem()
        {
            // Create saves directory if it doesn't exist
            if (!Directory.Exists(SAVE_DIRECTORY))
            {
                Directory.CreateDirectory(SAVE_DIRECTORY);
            }
        }

        public void SaveGame(SaveData data)
        {
            try
            {
                string savePath = Path.Combine(SAVE_DIRECTORY, SAVE_FILE);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(savePath, json);
                
                System.Diagnostics.Debug.WriteLine($"Game saved successfully to {savePath}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public SaveData LoadGame()
        {
            try
            {
                string savePath = Path.Combine(SAVE_DIRECTORY, SAVE_FILE);
                if (File.Exists(savePath))
                {
                    string json = File.ReadAllText(savePath);
                    var saveData = JsonConvert.DeserializeObject<SaveData>(json);
                    System.Diagnostics.Debug.WriteLine($"Game loaded successfully from {savePath}");
                    return saveData;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading game: {ex.Message}");
            }
            
            return new SaveData();
        }

        public bool SaveExists()
        {
            string savePath = Path.Combine(SAVE_DIRECTORY, SAVE_FILE);
            return File.Exists(savePath);
        }

        public void DeleteSave()
        {
            try
            {
                string savePath = Path.Combine(SAVE_DIRECTORY, SAVE_FILE);
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                    System.Diagnostics.Debug.WriteLine("Save file deleted successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting save file: {ex.Message}");
            }
        }
    }
}

// Mover a MemoriesOfTheVoid.Core