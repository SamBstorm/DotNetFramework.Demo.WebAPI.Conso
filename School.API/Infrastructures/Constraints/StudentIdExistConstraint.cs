using School.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace School.API.Infrastructures.Constraints
{
    public class StudentIdExistConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            object valueConstraint = values[parameterName];
            if (valueConstraint is null) return false;
            if(!int.TryParse(valueConstraint.ToString(), out int searchedId)) return false;
            return StudentController.students.Select(s => s.Id).Contains(searchedId);
        }
    }
}