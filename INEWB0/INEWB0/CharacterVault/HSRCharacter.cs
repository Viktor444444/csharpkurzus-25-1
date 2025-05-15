using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public class HSRCharacter : Character
    {
        public required string Path { get; set; }
        public required string Faction { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} - {Path} - {Faction} - HSR";
        }
    }

}
