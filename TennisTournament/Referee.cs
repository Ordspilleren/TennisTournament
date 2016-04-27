using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Referee : IPerson
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public Gender Gender { get; set; }
        public DateTime LicenceAcquired { get; set; }
        public DateTime LicenceRenewed { get; set; }

        public Referee(string firstName, string middleName, string lastName, DateTime birthDate, string nationality, Gender gender, DateTime licenceAcquired, DateTime licenceRenewed)
        {
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Nationality = nationality;
            this.Gender = gender;
            this.LicenceAcquired = licenceAcquired;
            this.LicenceRenewed = licenceRenewed;
        }
    }
}
