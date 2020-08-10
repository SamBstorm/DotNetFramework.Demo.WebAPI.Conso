using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.API.Models
{
    public static class MockUp
    {
        public static List<Student> students = new List<Student> {
            new Student{ Id = 1, FirstName="Samuel", LastName = "Legrain"},
            new Student{ Id = 2, FirstName="Cédric", LastName = "Pietquin"},
            new Student{ Id = 3, FirstName="Hanen", LastName = "Ben Hassine"},
            new Student{ Id = 4, FirstName="Jérémy", LastName = "Samyn"},
            new Student{ Id = 5, FirstName="Olivier", LastName = "Paquet"},
            new Student{ Id = 6, FirstName="Rachid", LastName = "Aferyad"},
            new Student{ Id = 7, FirstName="Victor", LastName = "Kabela"},
            new Student{ Id = 8, FirstName="Xavier", LastName = "Dubois"},
            new Student{ Id = 9, FirstName="Yusuf", LastName = "Ozdemir"}
        };

        public static List<User> users = new List<User>
        {
            new User{ Id = 1, Login = "TMorre", Password ="test1234=", IsAdmin = true},
            new User{ Id = 2, Login = "MPerson", Password ="test1234=", IsAdmin = true},
            new User{ Id = 3, Login = "LKhun", Password ="test1234=", IsAdmin = false},
            new User{ Id = 4, Login = "JGillain", Password ="test1234=", IsAdmin = true},
            new User{ Id = 5, Login = "SLorent", Password ="test1234=", IsAdmin = false},
            new User{ Id = 6, Login = "SLegrain", Password ="test1234=", IsAdmin = false}
        };
    }
}