using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    internal class Program
    {
        // TODO: Revise access modifiers (private, protected, public)
        // TODO: Generics
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var tournaments = new List<Tournament>();

            // NOTE: Not all functionality is implemeted in the UI.
            // This means that amount of players and the game types of a tournament cannot be set outside of the code.
            // The order of the player list also cannot be changed in the UI. By default they are listed by first name.
            // There is also no error checking, so the actions in the UI should be followed in order.

            var ui = new Ui();
            ConsoleKeyInfo cki;

            var malePlayers = ReadFiles.GetPlayers(@"data/MalePlayer.txt");
            var femalePlayers = ReadFiles.GetPlayers(@"data/FemalePlayer.txt");
            var allPlayers = malePlayers.Concat(femalePlayers);
            var referees = ReadFiles.GetReferees(@"data/refs.txt", 10);
            Tournament selectedtournament = null;

            do
            {
                ui.DisplayMenu(selectedtournament);
                cki = Console.ReadKey(true);

                switch (cki.KeyChar)
                {
                    case '1':
                        var tournament = ui.CreateTournament();
                        PlayerAssignHelper(tournament, allPlayers, GameTypes.Both, 8);
                        tournament.AddReferees(referees);
                        tournament.AddGameMaster(referees[0]);
                        tournaments.Add(tournament);
                        break;
                    case '2':
                        ui.ListTournaments(tournaments);
                        break;
                    case '3':
                        Console.WriteLine("Please enter the ID of the tournament that should be selected:");
                        selectedtournament = tournaments[int.Parse(Console.ReadKey().KeyChar.ToString())-1];
                        break;
                    case '4':
                        selectedtournament.Simulate(true);
                        selectedtournament.Simulate(false);
                        Console.WriteLine($"Tournament '{selectedtournament.Name}' has been simulated!");
                        break;
                    case '5':
                        ui.ListWinners(selectedtournament);
                        break;
                    case '6':
                        ui.ListMatches(selectedtournament);
                        break;
                    case '7':
                        ui.ListPlayers(selectedtournament);
                        break;
                }
            } while (cki.Key != ConsoleKey.Escape);
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
