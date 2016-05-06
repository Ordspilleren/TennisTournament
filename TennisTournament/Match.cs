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
        private int Sets { get; set; }
        private Team Team1 { get; set; }
        private Team Team2 { get; set; }
        // SetResults should probably be a dictionary with team and score instead
        public List<Tuple<int, int>> SetResults { get; private set; }
        private Referee Referee { get; set; }
        public Team Winner { get; private set; }

        public Match(Team team1, Team team2)
        {
            //this.MatchType = matchType;
            Team1 = team1;
            Team2 = team2;

            if (MatchType == Type.WSingle || MatchType == Type.WDouble)
            {
                Bo3 = true;
                Sets = 3;
            } else
            {
                Bo5 = true;
                Sets = 5;
            }
        }

        public void Play()
        {
            if (!IsValid())
            {
                throw new ArgumentException("The team composition that was input does not allow for a valid match. Match cannot be played.");
            }

            SetResults = new List<Tuple<int, int>>();
            
            // A custom seed is used here to avoid results being the same across sets (default Random class uses DateTime.now which is not good enough)
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            var team1SetsWon = 0;
            var team2SetsWon = 0;
            
            while ((team1SetsWon + team2SetsWon < Sets) && (team1SetsWon < 3) && (team2SetsWon < 3))
            {
                var team1Score = rnd.Next(1, 7);
                var team2Score = rnd.Next(1, 7);
                var result = new Tuple<int, int>(team1Score, team2Score);

                if ((team1Score == 6) && (team2Score < 6))
                {
                    team1SetsWon++;
                    SetResults.Add(result);
                }
                else if ((team2Score == 6) && (team1Score < 6))
                {
                    team2SetsWon++;
                    SetResults.Add(result);
                }
            }

            Winner = (team1SetsWon > team2SetsWon ? Team1 : Team2);
        }

        //public List<Player> GetWinner()
        //{
        //    int player1SetsWon = 0;
        //    int player2SetsWon = 0;
        //    foreach (var set in SetResults)
        //    {
        //        if (set.Item1 == 6 && set.Item2 <= 5)
        //        {
        //            player1SetsWon++;
        //        } else
        //        {
        //            player2SetsWon++;
        //        }
        //    }

        //    if (MatchType == Type.MSingle || MatchType == Type.WSingle)
        //    {
        //        if (player1SetsWon > player2SetsWon)
        //        {
        //            return new List<Player> { SinglePlayers[0] };
        //        } else
        //        {
        //            return new List<Player> { SinglePlayers[1] };
        //        }
        //    } else
        //    {
        //        if (player1SetsWon > player2SetsWon)
        //        {
        //            return new List<Player> { DoublePlayers[0].Item1, DoublePlayers[0].Item2 };
        //        } else
        //        {
        //            return new List<Player> { DoublePlayers[1].Item1, DoublePlayers[1].Item2 };
        //        }
        //    }
        //}

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

        public bool IsValid()
        {
            var result = false;
            if (Team1.IsDouble && Team2.IsDouble)
            {
                if ((Team1.Player1.Gender == Gender.Female && Team1.Player2.Gender == Gender.Female) && (Team2.Player1.Gender == Gender.Female && Team2.Player2.Gender == Gender.Female))
                {
                    MatchType = Type.WDouble;
                    result = true;
                }
                else if ((Team1.Player1.Gender == Gender.Male && Team1.Player2.Gender == Gender.Male) && (Team2.Player1.Gender == Gender.Male && Team2.Player2.Gender == Gender.Male))
                {
                    MatchType = Type.MDouble;
                    result = true;
                }
                else if ((Team1.Player1.Gender != Team1.Player2.Gender) && (Team2.Player1.Gender !=  Team2.Player2.Gender))
                {
                    MatchType = Type.MixDouble;
                    result = true;
                }
            }
            else if (!Team1.IsDouble && !Team2.IsDouble)
            {
                if (Team1.Player1.Gender == Gender.Female && Team2.Player1.Gender == Gender.Female)
                {
                    MatchType = Type.WSingle;
                    result = true;
                }
                else if (Team1.Player1.Gender == Gender.Male && Team2.Player1.Gender == Gender.Male)
                {
                    MatchType = Type.MSingle;
                    result = true;
                }
            }
            return result;
        }
    }
}
