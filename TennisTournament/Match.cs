using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Match
    {
        public enum Type { WSingle, MSingle, WDouble, MDouble, MixDouble }
        public bool Bo3;
        public bool Bo5;
        public int Team1Points

        public Match(List<Player> matchPlayers, Type matchType)
        {
            if (matchType == Type.WSingle || matchType == Type.WDouble)
            {
                Bo3 = true;
            } else
            {
                Bo5 = true;
            }

            Console.WriteLine("Match created! Players in match:");

            foreach (Player matchPlayer in matchPlayers)
            {
                Console.WriteLine(matchPlayer.FirstName);
            }
        }
    }
}
