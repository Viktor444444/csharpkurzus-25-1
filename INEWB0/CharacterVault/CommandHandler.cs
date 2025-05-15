using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public static class CommandHandler
    {
        public static async Task Run()
        {
            await PullDatabase.LoadAsync();
            await Database.LoadAsync();

            Console.WriteLine("CharacterVault ready. Type 'add', 'list', 'remove', 'pulladd', 'pulllist', 'pullremove', 'exit'.");

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                if (string.IsNullOrWhiteSpace(input)) continue;

                string[] parts = input.Split(' ', 2);
                string command = parts[0].ToLower();
                string args = parts.Length > 1 ? parts[1] : "";

                switch (command)
                {
                    case "add":
                        await AddCharacter();
                        break;
                    case "list":
                        ListCharacters();
                        break;
                    case "remove":
                        await RemoveCharacter(args);
                        break;
                    case "pulladd":
                        PullCommandHandler.AddPull();
                        break;
                    case "pulllist":
                        PullCommandHandler.ListPulls();
                        break;
                    case "pullremove":
                        PullCommandHandler.PullRemove();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        static async Task AddCharacter()
        {
            Console.Write("Which game? (genshin/hsr/wuwa): ");
            string? game = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(game))
            {
                Console.WriteLine("Invalid game input.");
                return;
            }

            Console.Write("Enter name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter element: ");
            string? element = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(element))
            {
                Console.WriteLine("Element cannot be empty.");
                return;
            }

            Console.Write("Enter rarity (number): ");
            if (!int.TryParse(Console.ReadLine(), out int rarity))
            {
                Console.WriteLine("Invalid rarity.");
                return;
            }

            Character character;

            switch (game)
            {
                case "genshin":
                    Console.Write("Enter weapon type: ");
                    string? weapon = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(weapon))
                    {
                        Console.WriteLine("Weapon type is required.");
                        return;
                    }

                    Console.Write("Enter region: ");
                    string? region = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(region))
                    {
                        Console.WriteLine("Region is required.");
                        return;
                    }

                    character = new GenshinCharacter
                    {
                        Name = name,
                        Element = element,
                        Rarity = rarity,
                        WeaponType = weapon,
                        Region = region
                    };
                    break;

                case "hsr":
                    Console.Write("Enter path: ");
                    string? path = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        Console.WriteLine("Path is required.");
                        return;
                    }

                    Console.Write("Enter faction: ");
                    string? faction = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(faction))
                    {
                        Console.WriteLine("Faction is required.");
                        return;
                    }

                    character = new HSRCharacter
                    {
                        Name = name,
                        Element = element,
                        Rarity = rarity,
                        Path = path,
                        Faction = faction
                    };
                    break;

                case "wuwa":
                    Console.Write("Enter weapon type: ");
                    string? wuwaWeapon = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(wuwaWeapon))
                    {
                        Console.WriteLine("Weapon type is required.");
                        return;
                    }

                    character = new Resonator
                    {
                        Name = name,
                        Element = element,
                        Rarity = rarity,
                        WeaponType = wuwaWeapon,
                    };
                    break;

                default:
                    Console.WriteLine("Unknown game. Try again.");
                    return;
            }

            Database.Characters.Add(character);
            await Database.SaveAsync();
            Console.WriteLine("Character added.");
        }

        static void ListCharacters()
        {
            foreach (var c in Database.Characters)
            {
                Console.WriteLine(c);
            }
        }

        static async Task RemoveCharacter(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please specify a name.");
                return;
            }

            var match = Database.Characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (match != null)
            {
                Database.Characters.Remove(match);
                await Database.SaveAsync();
                Console.WriteLine("Character removed.");
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }
    }
}
