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

            var malePlayers = ReadFiles.GetPlayers(@"data/MalePlayer.txt");
            var femalePlayers = ReadFiles.GetPlayers(@"data/FemalePlayer.txt");
            var allPlayers = malePlayers.Concat(femalePlayers);
            var referees = ReadFiles.GetReferees(@"data/refs.txt", 10);

            var tournament = new Tournament("Test", DateTime.Now, DateTime.Now, DateTime.Now);
            PlayerAssignHelper(tournament, allPlayers, GameTypes.Both, 64);
            tournament.AddReferees(referees);
            tournament.AddGameMaster(referees[0]);

            foreach (var player in tournament.ListPlayers())
            {
                Console.WriteLine(player.FirstName + player.Gender);
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

            foreach (var match in tournament.Matches)
            {
                Console.WriteLine(match.Winner.Player1.FirstName);
                Console.WriteLine(match.MatchType);
                Console.WriteLine(match.Referee.FirstName);
                Console.WriteLine(match.Round);
                foreach (var result in match.SetResults)
                {
                    Console.WriteLine(result.Item1 + ":" + result.Item2);
                }
            }



            Console.ReadKey();
        }

        private enum GameTypes { Singles, Doubles, Both }
        private static void PlayerAssignHelper(Tournament tournament, IEnumerable<Player> players, GameTypes gameTypes, int teamCount)
        {
            var randomizedPlayers = players.OrderBy(p => Guid.NewGuid()).ToList();
            switch (gameTypes)
            {
                case GameTypes.Singles:
                    tournament.AddPlayers(randomizedPlayers.GetRange(0, teamCount));
                    break;
                case GameTypes.Doubles:
                    for (int i = 0; i < teamCount*2; i += 2)
                    {
                        tournament.AddPlayers(randomizedPlayers[i], randomizedPlayers[i + 1]);
                    }
                    break;
                case GameTypes.Both:
                    tournament.AddPlayers(randomizedPlayers.GetRange(0, teamCount/2));
                    for (int i = teamCount/2; i < teamCount + teamCount/2; i += 2)
                    {
                        tournament.AddPlayers(randomizedPlayers[i], randomizedPlayers[i + 1]);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameTypes), gameTypes, null);
            }
        }
    }
}
