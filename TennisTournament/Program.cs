using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Program
    {
        // TODO: Add user interface to start simulation, add players etc.
        // TODO: Revise access modifiers (private, protected, public)
        // TODO: Generics
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //var henning = new Player("Henning", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            //var karsten = new Player("Karsten", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            //var gert = new Player("Gert", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            //var jens = new Player("Jens", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);
            //var lea = new Player("Lea", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);
            //var trine = new Player("Trine", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);
            //var hanne = new Player("Hanne", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);
            //var karoline = new Player("Karoline", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Female);

            //var lel = new Referee("Lars", "Middlename", "Lel", DateTime.Now, "Danish", Gender.Male, DateTime.Now, DateTime.Now);
            //var refs = new List<Referee> { lel };

            //var tournament1Players = new List<Player>() { henning, karsten, gert, trine, hanne, karoline, jens, lea };
            var players = ReadFiles.GetPlayers(@"data/MalePlayer.txt", @"data/FemalePlayer.txt", 96);
            var referees = ReadFiles.GetReferees(@"data/refs.txt", 10);
            var singlePlayers = players.GetRange(0, 32);
            var doublePlayers = players.GetRange(32, 64);
            var doublePlayersDone = new List<Tuple<Player, Player>>();

            for (int i = 0; i < doublePlayers.Count; i += 2)
            {
                doublePlayersDone.Add(new Tuple<Player, Player>(doublePlayers[i], doublePlayers[i+1]));
            }

            var tournament = new Tournament("Test", DateTime.Now, DateTime.Now, DateTime.Now);
            tournament.AddPlayers(singlePlayers);
            tournament.AddPlayers(doublePlayersDone);
            tournament.AddReferees(referees);
            tournament.AddGameMaster(referees[0]);

            foreach (var player in tournament.ListPlayers())
            {
                Console.WriteLine(player.FirstName);
            }

            Console.WriteLine("_________________________");

            tournament.Simulate(true);
            tournament.Simulate(false);
            foreach (var team in tournament.Winner)
            {
                if (!team.IsDouble)
                {
                    Console.WriteLine(team.Player1.FirstName);
                }
                else
                {
                    Console.WriteLine(team.Player1.FirstName + " AND " + team.Player2.FirstName);
                }
            }

            Console.WriteLine("___________________");

            // SetResults are not printed properly for some reason
            foreach (var match in tournament.Matches)
            {
                Console.WriteLine(match.Winner.Player1.FirstName);
                Console.WriteLine(match.MatchType);
                foreach (var result in match.SetResults)
                {
                    Console.WriteLine(result.Item1 + ":" + result.Item2);
                }
            }

            Console.ReadKey();
        }
    }
}
