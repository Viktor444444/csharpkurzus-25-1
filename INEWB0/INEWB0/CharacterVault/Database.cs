using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CharacterVault
{
    public static class Database
    {
        public static List<Character> Characters = new();
        private static string _filePath = "characters.json";

        public static async Task LoadAsync()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = await File.ReadAllTextAsync(_filePath); 
                    Characters = JsonSerializer.Deserialize<List<Character>>(json) ?? new List<Character>();
                }
                else
                {
                    await SaveAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public static async Task SaveAsync()
        {
            try
            {
                string json = JsonSerializer.Serialize(Characters, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
    }
}
