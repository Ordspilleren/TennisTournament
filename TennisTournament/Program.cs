using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Program
    {
        static void Main(string[] args)
        {
            Player henning = new Player("Henning", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player karsten = new Player("Karsten", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player gert = new Player("Gert", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player trine = new Player("Trine", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player hanne = new Player("Hanne", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player karoline = new Player("Karoline", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player jens = new Player("Jens", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            Player karl = new Player("Karl", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);

            List<Player> tournament1Players = new List<Player>() { henning, karsten, gert, trine, hanne, karoline, jens, karl };
            Tournament tournament1 = new Tournament(tournament1Players);
        }
    }
}
