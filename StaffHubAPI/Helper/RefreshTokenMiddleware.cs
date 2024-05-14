namespace StaffHubAPI.Helper
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public RefreshTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Services.Interfaces.IAuthenticationService authService)
        {
            var refreshToken = context.Request.Cookies["refreshToken"];

            if (refreshToken != null)
            {
                // Lấy token mới từ refresh token
                var newToken = authService.RefreshToken(refreshToken);
                if (newToken != null)
                {
                    // Gửi lại token mới trong header
                    context.Response.Headers.Add("Authorization", "Bearer " + newToken);
                }
            }

            await _next(context);
        }
    }
}