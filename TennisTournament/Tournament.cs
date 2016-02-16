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
        public Player[] Players;

        public Tournament()
        {
            for (int i = 1; i < Players.Length; i += 2)
            {
                Player[] matchPlayers = new Player[2];
                matchPlayers[0] = Players[i - 1];
                matchPlayers[1] = Players[i];
                Game match = new Game(matchPlayers);
            }
        }
    }
}
