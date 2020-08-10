using School.API.Infrastructures.Authentication;
using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace School.API.Controllers
{
    public class StudentController : ApiController
    {
        public static List<Student> students { get => MockUp.students; }
        private static List<User> users { get => MockUp.users; }

        //GET : api/Student/
        public IEnumerable<Student> Get()
        {
            return students;
        }

        //GET : api/Student/{id}
        public Student Get(int id)
        {
            return students.Where(s => s.Id == id).SingleOrDefault();
        }

        //POST : api/Student/
        public int Post(Student entity)
        {
            entity.Id = students.Max(s => s.Id) + 1;
            students.Add(entity);
            return entity.Id;
        }

        //PUT : api/Student/{id}
        public void Put(int id, Student entity)
        {
            Student s = Get(id);
            if (s is null) return; //Exception
            s.FirstName = entity.FirstName;
            s.LastName = entity.LastName;
        }

        //DELETE : api/Student/{id}
        [BasicAuthenticator("Admin")]
        public void Delete(int id)
        {
            Student s = Get(id);
            if (s is null) return; //Exception
            students.Remove(s);
        }
    }
}
