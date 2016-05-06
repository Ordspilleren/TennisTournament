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
        private Referee GameMaster { get; set; }
        public List<Match> Matches { get; private set; }
        public Team Winner { get; private set; }

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
                // Some proper errors should probably be used instead of return
                return;
            }
        }

        //public List<Player> ListPlayers(bool firstName = true)
        //{
        //    var sortedPlayers = firstName ? Players.OrderBy(x => x.FirstName).ToList() : Players.OrderBy(x => x.LastName).ToList();

        //    return sortedPlayers;
        //}

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

        public void Simulate()
        {
            var currentTeams = new List<Team>();
            var currentMatches = new List<Match>();
            Matches = new List<Match>();
            //Winner = new List<Player>();
            currentTeams = Teams;

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

            Winner = currentTeams[0];
        }
    }
}
