using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public static class PullCommandHandler
    {
        public static async Task AddPull()
        {
            Console.Write("Character name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Character name cannot be empty.");
                return;
            }

            Console.Write("Pity: ");
            string? pityInput = Console.ReadLine();
            if (!int.TryParse(pityInput, out int pity))
            {
                Console.WriteLine("Invalid pity number, defaulting to 0.");
                pity = 0;
            }

            Console.Write("Game (genshin/hsr/wuwa): ");
            string? game = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(game))
            {
                Console.WriteLine("Game cannot be empty.");
                return;
            }

            Console.Write("Time (you can write anything): ");
            string? time = Console.ReadLine() ?? "";

            var pull = new Pull(name, pity, game, time);
            PullDatabase.Pulls.Add(pull);
            await PullDatabase.SaveAsync();
            Console.WriteLine("Pull saved.");
        }

        public static void ListPulls()
        {
            var sorted = PullDatabase.Pulls.OrderBy(p => p.Pity)
                                           .ThenBy(p => p.CharacterName);

            foreach (var p in sorted)
            {
                Console.WriteLine($"{p.Game} — {p.CharacterName} (Pity: {p.Pity}) at '{p.Time}'");
            }

            Console.WriteLine();

            var stats = PullDatabase.Pulls
                .GroupBy(p => p.Game)
                .Select(g => new {
                    Game = g.Key,
                    Count = g.Count(),
                    MaxPity = g.Max(p => p.Pity),
                    AvgPity = g.Average(p => p.Pity)
                });

            foreach (var g in stats)
            {
                Console.WriteLine($"{g.Game}: {g.Count} pulls, max pity {g.MaxPity}, avg pity {g.AvgPity:F1}");
            }
        }

        public static async Task PullRemove()
        {
            Console.Write("Enter the character name of the pull to remove: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            var pullToRemove = PullDatabase.Pulls.FirstOrDefault(p => p.CharacterName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (pullToRemove != null)
            {
                PullDatabase.Pulls.Remove(pullToRemove);
                await PullDatabase.SaveAsync();
                Console.WriteLine("Pull removed successfully.");
            }
            else
            {
                Console.WriteLine("Pull not found.");
            }
        }
    }
}