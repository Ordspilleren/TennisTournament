//## Documentation

//#### Interface: IPerson
//This interface defines the general properties of a person, used in the Player and Referee classes.

//#### Class: Referee
//The Referee class implements the properties of the IPerson interface, and also contains the referee-specific functionality.

//#### Class: Player
//This class also implements the properties of IPerson, in addition to player-specific functionality.

//#### Class: Team
//The Team class takes up to two players and defines them as a team.This class is also used for individual players.The class also contains the IsDouble property which is set depending on the amount of players in the team.

//#### Class: Match
//The Match class contains the methods relevant for an individual match in the tournament.Among these are methods for adding referees and validating the match. The class also contains the Play method, which will "play" the match and update it with the result of each set along with the winner of the match.

//#### Class: Tournament
//The Tournament class contains all functionality for a tournament.This includes lists containing referees and teams in the tournament. Furthermore, it also contains a list with all matches played in the tournament and a list with the winner(s) of the tournament.The players in a tournament are always part of a team, even if they are alone(singles). These teams are instantiated upon adding players using the AddPlayers method.The class also contains methods for adding a Game Master, adding referees and simulating the tournament.

//#### Class: ReadFiles
//This class contains two methods, one for getting a list of players from one of the provided files, another for getting a list of referees, also from the provided file.

//#### Class: UI
//The UI class has methods for displaying information about the tournament in the console window.The goal of this class is to split up the core tournament functionality from actually displaying the information.

//#### Assumptions
//* I have assumed that a tournament is to be simulated in one go. That is, one round in the tournament cannot be simulated without also simulating the other rounds.
//* I have assumed that all teams (players) have an equal chance to win a match or tournament.

//#### Sources

//* The method for getting the age of a player is made with heavy inspiration from this StackOverflow post: http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c

//* The code for randomly selecting a gender for a referee in the ReadFiles.GetReferees method is based on this StackOverflow post: http://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    internal class Tournament
    {
        public string Name { get; private set; }
        public int Year { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        private List<Referee> Referees { get; }
        public List<Team> Teams { get; }
        private static readonly int[] TeamCount = { 8, 16, 32, 64 };
        private Referee GameMaster { get; set; }
        public List<Match> Matches { get; }
        public List<Team> Winner { get; }

        public Tournament(string name, int year, DateTime dateFrom, DateTime dateTo)
        {
            Name = name;
            Year = year;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Teams = new List<Team>();
            Referees = new List<Referee>();
            Winner = new List<Team>();
            Matches = new List<Match>();
        }

        public void AddPlayers(Player player)
        {
            Teams.Add(new Team(player));
        }

        public void AddPlayers(Player player1, Player player2)
        {
            Teams.Add(new Team(player1, player2));
        }

        public void AddPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                Teams.Add(new Team(player));
            }
        }

        public void AddPlayers(List<Tuple<Player, Player>> players)
        {
            foreach (var team in players)
            {
                Teams.Add(new Team(team.Item1, team.Item2));
            }
        }

        public void RemoveTeam(Team team)
        {
            Teams.Remove(team);
        }

        public void AddReferees(Referee referee)
        {
            Referees.Add(referee);
        }

        public void AddReferees(List<Referee> referees)
        {
            Referees.AddRange(referees);
        }

        public void RemoveReferee(Referee referee)
        {
            Referees.Remove(referee);
        }

        // This method will add a Game Master if one doesn't excist, or replace the existing one.
        public void AddGameMaster(Referee referee)
        {
            if (Referees.Contains(referee))
            {
                GameMaster = referee;
            } else
            {
                Console.WriteLine("This referee does not excist in the tournament, and can therefore not be set as Game Master");
            }
        }

        public bool IsOver()
        {
            var multipleTypes = Teams.Any(team => team.IsDouble) && Teams.Any(team => !team.IsDouble);
            var result = Winner.Any();

            if (multipleTypes && Winner.Count != 2)
            {
                result = false;
            }
            return result;
        }

        public List<Player> ListPlayers(bool firstName = true)
        {
            var players = new List<Player>();
            foreach (var team in Teams)
            {
                players.Add(team.Player1);

                if (team.IsDouble)
                {
                    players.Add(team.Player2);
                }
            }
            var sortedPlayers = firstName ? players.OrderBy(x => x.FirstName).ToList() : players.OrderBy(x => x.LastName).ToList();

            return sortedPlayers;
        }

        private List<Match> InitializeMatches(List<Team> teams)
        {
            var rnd = new Random();
            var assignedTeams = new List<Team>();
            var matches = new List<Match>();
            while (assignedTeams.Count < teams.Count)
            {
                var team1 = teams[rnd.Next(teams.Count)];
                var team2 = teams[rnd.Next(teams.Count)];
                var match = new Match(team1, team2);
                match.AddReferee(Referees[rnd.Next(Referees.Count)]);
                if (!assignedTeams.Contains(team1) && !assignedTeams.Contains(team2) && match.IsValid())
                {
                    //var players = new List<Player> { team1, team2 };
                    assignedTeams.Add(team1);
                    assignedTeams.Add(team2);
                    matches.Add(match);
                }
            }

            return matches;
        }

        public void Simulate(bool doubles)
        {
            if (!TeamCount.Contains(Teams.Count))
            {
                Console.WriteLine($"There is not an allowed number of teams in the tournament. Current amount: {Teams.Count}");
                return;
            }
            if (!Referees.Any())
            {
                Console.WriteLine("Please add at least one referee to the tournament");
                return;
            }
            if (GameMaster == null)
            {
                Console.WriteLine("A Game Master has not been set.");
                return;
            }

            var currentMatches = new List<Match>();
            var currentTeams = doubles ? Teams.Where(team => team.IsDouble).ToList() : Teams.Where(team => !team.IsDouble).ToList();
            var round = 1;

            while (currentTeams.Count > 1)
            {
                var initializedMatches = InitializeMatches(currentTeams);
                Matches.AddRange(initializedMatches);
                currentMatches.AddRange(initializedMatches);
                currentTeams.Clear();
                foreach (var match in currentMatches)
                {
                    match.Play(round);
                    currentTeams.Add(match.Winner);
                }
                currentMatches.Clear();
                round++;
            }

            Winner.Add(currentTeams[0]);
        }
    }
}
