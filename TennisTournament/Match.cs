using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public List<Tuple<int, int>> SetResults { get; private set; }
        private Referee Referee { get; set; }
        public List<Player> Winner { get; private set; }

        public Match(Type matchType, List<Tuple<Player, Player>> doublePlayers) : this(matchType)
        {
            this.DoublePlayers = doublePlayers;
        }

        public Match(Type matchType, List<Player> singlePlayers) : this(matchType)
        {
            this.SinglePlayers = singlePlayers;
        }

        public Match(Type matchType)
        {
            this.MatchType = matchType;
            if (matchType == Type.WSingle || matchType == Type.WDouble)
            {
                Bo3 = true;
            } else
            {
                Bo5 = true;
            }
        }

        public void Play()
        {
            SetResults = new List<Tuple<int, int>>();
            Winner = new List<Player>();
            var player1SetsWon = 0;
            var player2SetsWon = 0;
            // A custom seed is used here to avoid results being the same across sets (default Random class uses DateTime.now which is not good enough)
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            if (Bo3)
            {
                while (player1SetsWon + player2SetsWon < 3)
                {
                    var player1Score = rnd.Next(1, 7);
                    var player2Score = rnd.Next(1, 7);
                    var result = new Tuple<int, int>(player1Score, player2Score);

                    if ((player1Score == 6) && (player2Score < 6))
                    {
                        player1SetsWon++;
                        SetResults.Add(result);
                    }
                    else if ((player2Score == 6) && (player1Score < 6))
                    {
                        player2SetsWon++;
                        SetResults.Add(result);
                    }
                }
            }
            else
            {
                
            }

            Winner.Add(player1SetsWon > player2SetsWon ? SinglePlayers[0] : SinglePlayers[1]);
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
            } else if (MatchType == Type.MDouble)
            {
                return ((DoublePlayers[0].Item1.Gender == Gender.Male && DoublePlayers[0].Item2.Gender == Gender.Male) && (DoublePlayers[1].Item1.Gender == Gender.Male && DoublePlayers[1].Item2.Gender == Gender.Male));
            }
            return false;
        }
    }
}
