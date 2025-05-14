using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CharacterVault
{
    public static class PullDatabase
    {
        public static List<Pull> Pulls = new();
        private static string _filePath = "pulls.json";

        public static async Task LoadAsync()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = await File.ReadAllTextAsync(_filePath);
                    Pulls = JsonSerializer.Deserialize<List<Pull>>(json) ?? new();
                }
                else
                {
                    await SaveAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading pulls: {ex.Message}");
            }
        }

        public static async Task SaveAsync()
        {
            try
            {
                string json = JsonSerializer.Serialize(
                    Pulls.OrderBy(p => p.CharacterName).ToList(),
                    new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving pulls: {ex.Message}");
            }
        }
    }
}
