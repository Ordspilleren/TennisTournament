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
        private List<Player> Players { get; set; }
        private Referee GameMaster { get; set; }
        private List<Match> Matches { get; set; }

        public Tournament(string name, DateTime year, DateTime dateFrom, DateTime dateTo, List<Referee> referees, List<Player> players)
        {
            this.Name = name;
            this.Year = Year;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.Referees = referees;
            this.Players = players;

            this.Matches = InitializeMatches();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            //var item = Players.Find(x => x.FirstName == player.FirstName && x.MiddleName == player.MiddleName && x.LastName == player.LastName);
            Players.Remove(player);
        }

        public void AddReferee(Referee referee)
        {
            Referees.Add(referee);
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

        public List<Player> ListPlayers(bool firstName = true)
        {
            List<Player> sortedPlayers;
            if (firstName)
            {
                sortedPlayers = Players.OrderBy(x => x.FirstName).ToList();
            } else
            {
                sortedPlayers = Players.OrderBy(x => x.LastName).ToList();
            }

            return sortedPlayers;
        }

        private List<Match> InitializeMatches()
        {
            Random rnd = new Random();
            List<Player> assignedPlayers = new List<Player>();
            List<Match> matches = new List<Match>();

            while (assignedPlayers.Count < Players.Count)
            {
                var player1 = Players[rnd.Next(Players.Count)];
                var player2 = Players[rnd.Next(Players.Count)];
                if (!assignedPlayers.Contains(player1) && !assignedPlayers.Contains(player2))
                {
                    List<Player> players = new List<Player> { player1, player2 };
                    assignedPlayers.AddRange(players);
                    Match match = new Match(Match.Type.MSingle, players);
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

        }
    }
}
