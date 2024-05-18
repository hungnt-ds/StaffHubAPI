using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StaffHubAPI.Helper.Attributes
{
    public class AuthorizeClaimAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _claim;

        public AuthorizeClaimAttribute(string claim)
        {
            _claim = claim;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim && c.Value == "true");

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
