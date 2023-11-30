using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace WebStudentAPI.MiddleWares
{
    public class AuthenticationMiddlewaree
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddlewaree(RequestDelegate next)
        {
            _next = next;   
        }

        public async Task Invoke(HttpContext context)
        {
            string autHeader = context.Request.Headers["Authorization"];
            if(autHeader == null)
            {
                context.Response.StatusCode=StatusCodes.Status401Unauthorized;
                return;
            }
            if (autHeader!=null &&autHeader.StartsWith("basic",StringComparison.OrdinalIgnoreCase))
            {
                var token = autHeader.Substring(6).Trim();
                string crediantialString = "";
                try
                {
                    crediantialString=Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch(Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    return;
                }

                var credentials = crediantialString.Split(':');
                var username = credentials[0];
                var password = credentials[1];
                if (username == "elcin2003" && password == "12345")
                {
                    var claim = new[]
                    {
                        new Claim("username", username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var identity = new ClaimsIdentity(claim, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                await _next(context);
            }
        }
    }
}
