using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Tournament
    {
        public string Name;
        public DateTime Year;
        public DateTime DateFrom;
        public DateTime DateTo;
        public List<Player> Players;
        public List<Match> Matches = new List<Match>();

        public Tournament(List<Player> tournamentPlayers)
        {
            this.Players = tournamentPlayers;

            // 
            List<Player> matchPlayers = new List<Player>();
            for (int i = 1; i < Players.Count; i += 2)
            {
                matchPlayers.Add(Players[i - 1]);
                matchPlayers.Add(Players[i]);
                Matches.Add(new Match(matchPlayers, Match.Type.MixDouble));
                matchPlayers.Clear();
            }
        }
    }
}
