using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    internal class Match
    {
        public enum Type { WSingle, MSingle, WDouble, MDouble, MixDouble }
        public Type MatchType { get; private set; }
        private bool Bo3 { get; set; }
        private bool Bo5 { get; set; }
        private int Sets { get; }
        public bool IsDouble { get; private set; }
        public Team Team1 { get; }
        public Team Team2 { get; }
        // SetResults should probably be a dictionary with team and score instead
        public List<Tuple<int, int>> SetResults { get; private set; }
        public int Round { get; private set; }
        public Referee Referee { get; private set; }
        public Team Winner { get; private set; }

        public Match(Team team1, Team team2)
        {
            //this.MatchType = matchType;
            Team1 = team1;
            Team2 = team2;

            if (IsValid() && (MatchType == Type.WSingle || MatchType == Type.WDouble))
            {
                Bo3 = true;
                Sets = 3;
            } else
            {
                Bo5 = true;
                Sets = 5;
            }
        }

        public void Play(int round = 0)
        {
            if (!IsValid())
            {
                throw new ArgumentException("The team composition that was input does not allow for a valid match. Match cannot be played.");
            }
            if (round != 0)
            {
                Round = round;
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

        public int SetsPlayed()
        {
            return SetResults.Count;
        }

        public void AddReferee(Referee referee)
        {
            Referee = referee;
        }

        public void RemoveReferee()
        {
            Referee = null;
        }

        public bool IsValid()
        {
            var result = false;
            if (Team1.IsDouble && Team2.IsDouble)
            {
                IsDouble = true;
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
