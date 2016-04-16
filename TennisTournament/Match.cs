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
        private Type MatchType { get; set; }
        private bool Bo3 { get; set; }
        private bool Bo5 { get; set; }
        private List<Player> SinglePlayers { get; set; }
        private List<Tuple<Player, Player>> DoublePlayers { get; set; }
        private List<Tuple<int, int>> SetResults { get; set; }
        private Referee Referee { get; set; }

        public Match(Type matchType, List<Tuple<Player, Player>> doublePlayers, List<Tuple<int, int>> setResults) : this(matchType, setResults)
        {
            this.DoublePlayers = doublePlayers;
        }

        public Match(Type matchType, List<Player> singlePlayers, List<Tuple<int, int>> setResults) : this(matchType, setResults)
        {
            this.SinglePlayers = singlePlayers;
        }

        public Match(Type matchType, List<Tuple<int, int>> setResults)
        {
            this.MatchType = matchType;
            this.SetResults = setResults;
            if (matchType == Type.WSingle || matchType == Type.WDouble)
            {
                Bo3 = true;
            } else
            {
                Bo5 = true;
            }
        }

        public List<Player> GetWinner()
        {
            int player1SetsWon = 0;
            int player2SetsWon = 0;
            foreach (var set in SetResults)
            {
                if (set.Item1 == 6 && set.Item2 <= 5)
                {
                    player1SetsWon++;
                } else
                {
                    player2SetsWon++;
                }
            }

            if (MatchType == Type.MSingle || MatchType == Type.WSingle)
            {
                if (player1SetsWon > player2SetsWon)
                {
                    return new List<Player> { SinglePlayers[0] };
                } else
                {
                    return new List<Player> { SinglePlayers[1] };
                }
            } else
            {
                if (player1SetsWon > player2SetsWon)
                {
                    return new List<Player> { DoublePlayers[0].Item1, DoublePlayers[0].Item2 };
                } else
                {
                    return new List<Player> { DoublePlayers[1].Item1, DoublePlayers[1].Item2 };
                }
            }
        }

        // Needs to be refined to support sets > actual sets played
        public int SetsPlayed()
        {
            return SetResults.Count;
        }

        public void AddReferee(Referee referee)
        {
            this.Referee = referee;
        }

        public void RemoveReferee()
        {
            this.Referee = null;
        }

        public bool ValidateType()
        {
            if (MatchType == Type.MSingle)
            {
                return (SinglePlayers[0].Gender == Gender.Male && SinglePlayers[1].Gender == Gender.Male);
            }
            return false;
        }
    }
}
