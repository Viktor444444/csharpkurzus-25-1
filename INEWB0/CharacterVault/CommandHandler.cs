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
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;

                string[] parts = input.Split(' ', 2);
                string command = parts[0].ToLower();
                string args = parts.Length > 1 ? parts[1] : "";

                switch (command)
                {
                    case "add":
                        AddCharacter();
                        break;
                    case "list":
                        ListCharacters();
                        break;
                    case "remove":
                        RemoveCharacter(args);
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

        static void AddCharacter()
        {
            Console.Write("Which game? (genshin/hsr/wuwa): ");
            string game = Console.ReadLine()?.Trim().ToLower();

            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter element: ");
            string element = Console.ReadLine();

            Console.Write("Enter rarity (number): ");
            int.TryParse(Console.ReadLine(), out int rarity);

            Character character;

            switch (game)
            {
                case "genshin":
                    Console.Write("Enter weapon type: ");
                    string weapon = Console.ReadLine();

                    Console.Write("Enter region: ");
                    string region = Console.ReadLine();

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
                    string path = Console.ReadLine();

                    Console.Write("Enter faction: ");
                    string faction = Console.ReadLine();

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
                    string wuwaWeapon = Console.ReadLine();

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
            Database.SaveAsync();
            Console.WriteLine("Character added.");
        }

        static void ListCharacters()
        {
            foreach (var c in Database.Characters)
            {
                Console.WriteLine(c);
            }
        }

        static void RemoveCharacter(string name)
        {
            var match = Database.Characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (match != null)
            {
                Database.Characters.Remove(match);
                Database.SaveAsync();
                Console.WriteLine("Character removed.");
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }
    }
}
