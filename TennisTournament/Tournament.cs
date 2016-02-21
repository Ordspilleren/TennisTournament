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
        public List<Game> Matches = new List<Game>();

        public Tournament(List<Player> tournamentPlayers)
        {
            this.Players = tournamentPlayers;

            // 
            List<Player> matchPlayers = new List<Player>();
            for (int i = 1; i < Players.Count; i += 2)
            {
                matchPlayers.Add(Players[i - 1]);
                matchPlayers.Add(Players[i]);
                Matches.Add(new Game(matchPlayers, Game.Type.MixDouble));
                matchPlayers.Clear();
            }
        }
    }
}
