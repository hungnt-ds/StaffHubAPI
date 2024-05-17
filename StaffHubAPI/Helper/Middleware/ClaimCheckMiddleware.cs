using StaffHubAPI.DataAccess;
using StaffHubAPI.Services.Implementations;
using System.Security.Claims;

namespace StaffHubAPI.Helper.Middleware
{
    public class ClaimCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userName = context.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                //var userId = int.Parse(context.User.Claims.First(c => c.Type == "UserId").Value);
                var userId = int.Parse(context.User.Claims.First(c => c.Type == "UserId").Value);
                var userService = new UserClaimService(dbContext);
                var userClaims = await userService.GetUserClaimsAsync(userId);

                var claimsIdentity = context.User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    foreach (var claim in userClaims)
                    {
                        claimsIdentity.AddClaim(new Claim(claim, "true"));
                    }
                }
            }

            await _next(context);
        }
    }

}
