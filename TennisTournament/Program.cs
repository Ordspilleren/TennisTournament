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
            var henning = new Player("Henning", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            var karsten = new Player("Karsten", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            var gert = new Player("Gert", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            var jens = new Player("Jens", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            var lea = new Player("Karl", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);
            var trine = new Player("Trine", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);
            var hanne = new Player("Hanne", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);
            var karoline = new Player("Karoline", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);

            var lel = new Referee("Lars", "Middlename", "Lel", DateTime.Now, "Danish", Gender.Male, DateTime.Now, DateTime.Now);
            var refs = new List<Referee> { lel };

            var tournament1Players = new List<Player>() { henning, karsten, gert, trine, hanne, karoline, jens, lea };
            var tournament = new Tournament("Test", DateTime.Now, DateTime.Now, DateTime.Now);
            tournament.AddPlayers(tournament1Players);
            tournament.AddReferees(refs);

            tournament.Simulate();
            Console.WriteLine(tournament.Winner.Player1.FirstName);

            // SetResults are not printed properly for some reason
            foreach (var match in tournament.Matches)
            {
                Console.WriteLine(match.Winner.Player1.FirstName);
                foreach (var result in match.SetResults)
                {
                    Console.WriteLine(result.Item1 + ":" + result.Item2);
                }
            }

            Console.ReadKey();

            //List<Player> tournament1Players = new List<Player>() { henning, karsten, gert, trine, hanne, karoline, jens, karl };
            //Tournament tournament1 = new Tournament(tournament1Players);

            //var gamePlayers = new List<Tuple<Player, Player>>();
            //gamePlayers.Add(new Tuple<Player, Player>(henning, karsten));
            //gamePlayers.Add(new Tuple<Player, Player>(gert, karl));
            //var sets = new List<Tuple<int, int>>();
            //sets.Add(new Tuple<int, int>(6, 2));
            //sets.Add(new Tuple<int, int>(3, 6));
            //sets.Add(new Tuple<int, int>(5, 6));

            //Match match = new Match(Match.Type.MDouble, gamePlayers, sets);
            //Console.WriteLine(match.ValidateType());

            //foreach (Player player in match.GetWinner())
            //{
            //    Console.WriteLine(player.FirstName);
            //}
        }
    }
}
