using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public class Character
    {
        public string Name { get; set; }
        public string Element { get; set; }
        public int Rarity { get; set; }

        public Character() { }

        public override string ToString()
        {
            return $"{Name} - {Element} - {Rarity}";
        }
    }

}
