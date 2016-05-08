using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    internal class Ui
    {
        public void DisplayMenu(Tournament selectedTournament)
        {
            var selectedTournamentText = selectedTournament == null ? "None" : selectedTournament.Name;
            var selectedTournamentStatus = selectedTournament != null && selectedTournament.Winner.Any() ? "Completed" : "In progress";
            Console.WriteLine($@"
             _______________|MENU|________________________
            |                                             |
            | Choose what you want to do:                 |
            | 1) Create a new tournament.                 |
            | 2) List all tournaments.                    |
            | 3) Select a tournament.                     |
            | 4) Play selected tournament.                |
            | 5) List winner(s) of selected tournament.   |
            | 6) List all matches in selected tournament. |
            | 7) List all players in selected tournament. |
            | ESC) Exit.                                  |
            |_____________________________________________|
              SELECTED TOURNAMENT: {selectedTournamentText} ({selectedTournamentStatus})");
        }

        public Tournament CreateTournament()
        {
            Console.WriteLine("Please specify a name for the tournament:");
            var name = Console.ReadLine();
            Console.WriteLine("Please enter the year the tournament takes place:");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter a start date for the tournament in the format YYYY-MM-DD:");
            var dateFrom = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Please enter an end date for the tournament in the format YYYY-MM-DD:");
            var dateTo = DateTime.Parse(Console.ReadLine());

            return new Tournament(name, year, dateFrom, dateTo);
        }

        public void ListTournaments(List<Tournament> tournaments)
        {
            var i = 1;
            foreach (var tournament in tournaments)
            {
                Console.WriteLine($"_{i}_|{tournament.Name}|___");
                Console.WriteLine($"Year: {tournament.Year}");
                Console.WriteLine($"Start Date: {tournament.DateFrom}");
                Console.WriteLine($"End Date: {tournament.DateTo}");
                Console.WriteLine($"Teams: {tournament.Teams.Count}");
                i++;
            }
        }

        public void ListWinners(Tournament tournament)
        {
            foreach (var team in tournament.Winner)
            {
                if (!team.IsDouble)
                {
                    Console.WriteLine($"{team.Player1.FirstName} {team.Player1.MiddleName} {team.Player1.LastName}");
                }
                else
                {
                    Console.WriteLine($"{team.Player1.FirstName} {team.Player1.MiddleName} {team.Player1.LastName} & {team.Player2.FirstName} {team.Player2.MiddleName} {team.Player2.LastName}");
                }
            }
        }

        public void ListMatches(Tournament tournament)
        {
            foreach (var match in tournament.Matches)
            {
                Console.WriteLine("---Players---");
                if (!match.IsDouble)
                {
                    Console.WriteLine($"{match.Winner.Player1.FirstName} {match.Winner.Player1.MiddleName} {match.Winner.Player1.LastName}");
                }
                else
                {
                    Console.WriteLine($"{match.Winner.Player1.FirstName} {match.Winner.Player1.MiddleName} {match.Winner.Player1.LastName} & {match.Winner.Player2.FirstName} {match.Winner.Player2.MiddleName} {match.Winner.Player2.LastName}");
                }
                Console.WriteLine("---Match Type---");
                Console.WriteLine(match.MatchType);
                Console.WriteLine("---Referee---");
                Console.WriteLine(match.Referee.FirstName);
                Console.WriteLine("---Round---");
                Console.WriteLine(match.Round);
                Console.WriteLine("---Set Results---");
                foreach (var result in match.SetResults)
                {
                    Console.WriteLine(result.Item1 + ":" + result.Item2);
                }
                Console.WriteLine("___________________________________________________________");
            }
        }

        public void ListPlayers(Tournament tournament)
        {
            foreach (var player in tournament.ListPlayers())
            {
                Console.WriteLine($"{player.FirstName} {player.MiddleName} {player.LastName} (Age: {player.GetAge()})");
            }
        }
    }
}
