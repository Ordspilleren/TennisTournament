using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Program
    {
        static void Main(string[] args)
        {
            Player henning = new Player("Henning", "Gungadin", "Hansen", DateTime.Parse("1994-07-05"), "Danish", Gender.Male);

            Console.WriteLine(henning.GetAge());
        }
    }
}
