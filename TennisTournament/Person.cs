using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament
{
    public enum Gender { Male, Female }

    public interface IPerson
    {
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
        string Nationality { get; set; }
        Gender Gender { get; set; }
    }

    //public class Person : IPerson
    //{
    //    public string firstName { get; set; }
    //    public string middleName { get; set; }
    //    public string lastName { get; set; }
    //    public DateTime birthDate { get; set; }
    //    public string nationality { get; set; }
    //    public Gender gender { get; set; }
    //}
}
