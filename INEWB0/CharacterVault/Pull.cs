using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterVault
{
    public record class Pull(string CharacterName, int Pity, string Game, string Time);
}
