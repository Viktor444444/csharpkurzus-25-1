using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public class HSRCharacter : Character
    {
        public string Path { get; set; }
        public string Faction { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} - {Path} - {Faction} - HSR";
        }
    }

}
