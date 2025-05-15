using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public class Character
    {
        public required string Name { get; set; }
        public required string Element { get; set; }
        public required int Rarity { get; set; }

        public Character() { }

        public override string ToString()
        {
            return $"{Name} - {Element} - {Rarity}";
        }
    }

}
