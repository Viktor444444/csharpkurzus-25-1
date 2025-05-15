using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public class GenshinCharacter : Character
    {
        public required string WeaponType { get; set; }
        public required string Region { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} - {WeaponType} - {Region} - GI";
        }
    }

}
