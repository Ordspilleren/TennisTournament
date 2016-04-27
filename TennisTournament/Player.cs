using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Player : IPerson
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public Gender Gender { get; set; }

        public Player(string firstName, string middleName, string lastName, DateTime birthDate, string nationality, Gender gender)
        {
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Nationality = nationality;
            this.Gender = gender;
        }

        public int GetAge()
        {
            // http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
            var now = float.Parse(DateTime.Now.ToString("yyyy.MMdd"));
            var dob = float.Parse(BirthDate.ToString("yyyy.MMdd"));

            return (int)(now - dob);
        }
    }
}
