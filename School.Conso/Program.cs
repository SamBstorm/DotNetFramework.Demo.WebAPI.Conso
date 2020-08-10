using School.Conso.Infrastructures;
using School.Conso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Conso
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Veuillez patienter que l'API soit démarrée et ensuite appuis sur Enter...");
            Console.ReadLine();
            StudentService service = new StudentService("http://localhost:50701/api/");
            Student s1 = service.Get(1);
            Console.WriteLine($"{s1.Id} : {s1.LastName}, {s1.FirstName}");
            Console.ReadLine();

            service = new StudentService("http://localhost:50701/api/");
            Student newStu = new Student() { FirstName = "Toto", LastName = "C'est un petit garçon" };
            newStu.Id = service.Post(newStu);
            Console.WriteLine($"{newStu.Id} : {newStu.LastName}, {newStu.FirstName}");

            service = new StudentService("http://localhost:50701/api/");
            newStu.LastName = "Totorino";
            service.Put(newStu.Id,newStu);
            Console.WriteLine($"{newStu.Id} : {newStu.LastName}, {newStu.FirstName}");

            service = new StudentService("http://localhost:50701/api/", "TMorre", "test1234=");
            service.Delete(1);

            service = new StudentService("http://localhost:50701/api/");
            IEnumerable<Student> students = service.Get();
            foreach (Student s in students)
            {
                Console.WriteLine($"{s.Id} : {s.LastName}, {s.FirstName}");
            }
            Console.ReadLine();
        }
    }
}
