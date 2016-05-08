using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TennisTournament
{
    static class ReadFiles
    {
        public static List<Player> GetPlayers(string playerFile)
        {
            var players = new List<Player>();
            var gender = playerFile.ToLower().Contains("female") ? Gender.Female : Gender.Male;

            foreach (var line in File.ReadLines(playerFile))
            {
                var splitLine = line.Split('|');

                var player = new Player(int.Parse(splitLine[0]), splitLine[1], splitLine[2], splitLine[3], DateTime.Parse(splitLine[4]), splitLine[5], splitLine[6], gender);
                players.Add(player);
            }

            return players;
        }

        public static List<Referee> GetReferees(string refereeFile, int refereeLimit)
        {
            var referees = new List<Referee>();
            var rnd = new Random();
            foreach (var line in File.ReadLines(refereeFile).Take(refereeLimit))
            {
                var splitLine = line.Split('|');

                // A bit complicated just to randomize the gender of the referees, but hey, they can't all be males. http://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration
                var gender = (Gender) Enum.GetValues(typeof(Gender)).GetValue(rnd.Next(2));
                var referee = new Referee(int.Parse(splitLine[0]), splitLine[1], splitLine[2], splitLine[3], DateTime.Parse(splitLine[4]), splitLine[5], splitLine[6], gender, DateTime.Parse(splitLine[7]), DateTime.Parse(splitLine[8]));
                referees.Add(referee);
            }

            return referees;
        }
    }
}
