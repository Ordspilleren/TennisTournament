using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Team
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public bool IsDouble { get; private set; }

        public Team(Player player1)
        {
            Player1 = player1;
        }

        public Team(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            IsDouble = true;
        }
    }
}
