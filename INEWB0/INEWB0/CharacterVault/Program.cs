using System.Threading.Tasks;

namespace CharacterVault
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Database.LoadAsync();
            await PullDatabase.LoadAsync();

            await CommandHandler.Run();
        }
    }
}
