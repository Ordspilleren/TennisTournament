using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Tournament
    {
        private string Name { get; set; }
        private DateTime Year { get; set; }
        private DateTime DateFrom { get; set; }
        private DateTime DateTo { get; set; }
        private List<Referee> Referees { get; set; }
        //private List<Player> Players { get; set; }
        private List<Team> Teams { get; set; }
        private static readonly int[] TeamCount = { 8, 16, 32, 64 };
        private Referee GameMaster { get; set; }
        public List<Match> Matches { get; private set; }
        public List<Team> Winner { get; private set; }

        public Tournament(string name, DateTime year, DateTime dateFrom, DateTime dateTo)
        {
            this.Name = name;
            this.Year = Year;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            //this.Referees = referees;
            //this.Teams = teams;
            Teams = new List<Team>();
            Referees = new List<Referee>();
            Winner = new List<Team>();
            Matches = new List<Match>();
        }

        // TODO: There should be a check that ensures the total amount of teams is either 8, 16, 32 or 64
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

        //public void RemovePlayer(Player player)
        //{
        //    //var item = Players.Find(x => x.FirstName == player.FirstName && x.MiddleName == player.MiddleName && x.LastName == player.LastName);
        //    Players.Remove(player);
        //}

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

        // TODO: A referee should be assigned each match
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
                if (!assignedTeams.Contains(team1) && !assignedTeams.Contains(team2) && match.IsValid())
                {
                    //var players = new List<Player> { team1, team2 };
                    assignedTeams.Add(team1);
                    assignedTeams.Add(team2);
                    matches.Add(match);
                }
            }

            return matches;


            //for (int i = 0; i < Players.Count / 2;)
            //{

            //    List<Player> players = new List<Player> { Players[rnd.Next(Players.Count)], Players[rnd.Next(Players.Count)] };
            //    assignedPlayers.AddRange(players);
            //    Match match = new Match();
            //    i++;
            //}
        }

        // TODO: Possibly add a round counter and a round integer to Matches. This way there can be kept track of which round a match was played in.
        public void Simulate(bool doubles)
        {
            if (!TeamCount.Contains(Teams.Count))
            {
                Console.WriteLine("There is not an allowed number of teams in the tournament.");
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

            var currentTeams = new List<Team>();
            var currentMatches = new List<Match>();
            currentTeams = doubles ? Teams.Where(team => team.IsDouble).ToList() : Teams.Where(team => !team.IsDouble).ToList();

            while (currentTeams.Count > 1)
            {
                var initializedMatches = InitializeMatches(currentTeams);
                Matches.AddRange(initializedMatches);
                currentMatches.AddRange(initializedMatches);
                currentTeams.Clear();
                foreach (var match in currentMatches)
                {
                    match.Play();
                    currentTeams.Add(match.Winner);
                }
                currentMatches.Clear();
            }

            //Winner = winnerCount == 1 ? new List<Team>() { currentTeams[0] } : new List<Team>() { currentTeams[0], currentTeams[1] };
            Winner.Add(currentTeams[0]);
        }
    }
}
