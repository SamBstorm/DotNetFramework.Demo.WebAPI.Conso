using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace School.API.Infrastructures.Authentication
{
    public class BasicAuthenticatorAttribute : Attribute, IAuthenticationFilter
    {
        private List<User> _users { get => MockUp.users; }

        private readonly string realm;

        public BasicAuthenticatorAttribute(string realm)
        {
            this.realm = "realm=" + realm;
        }

        public bool AllowMultiple { get => false; }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage httpQuery = context.Request;
            try
            {
                if (httpQuery.Headers.Authorization is null) throw new UnauthorizedAccessException();
                if (!httpQuery.Headers.Authorization.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)) throw new UnauthorizedAccessException();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string credentials = encoding.GetString(Convert.FromBase64String(httpQuery.Headers.Authorization.Parameter));
                string[] parts = credentials.Split(':');
                if (parts.Length > 2) throw new UnauthorizedAccessException();
                string userLogin = parts[0].Trim();
                string userPassword = parts[1].Trim();
                if (userLogin == "") throw new UnauthorizedAccessException();
                User user = GetUser(userLogin, userPassword);
                if (user is null) throw new UnauthorizedAccessException();
                if (!(user.IsAdmin && this.realm =="realm=Admin")) throw new UnauthorizedAccessException();
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userLogin)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "basic");
                ClaimsPrincipal principal = new ClaimsPrincipal(new[] { identity });
                context.Principal = principal;
            }
            catch (UnauthorizedAccessException)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], httpQuery);
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result, realm);
            return Task.FromResult(0);
        }

        private User GetUser(string userLogin, string userPassword)
        {
            return _users.Where(u => u.Login == userLogin && u.Password == userPassword).SingleOrDefault();
        }
    }
}