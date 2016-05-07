using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    class Referee : IPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public Gender Gender { get; set; }
        public DateTime LicenceAcquired { get; set; }
        public DateTime LicenceRenewed { get; set; }

        public Referee(int id, string firstName, string middleName, string lastName, DateTime birthDate, string country, string countryCode, Gender gender, DateTime licenceAcquired, DateTime licenceRenewed)
        {
            Id = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Country = country;
            CountryCode = countryCode;
            this.Gender = gender;
            this.LicenceAcquired = licenceAcquired;
            this.LicenceRenewed = licenceRenewed;
        }
    }
}
