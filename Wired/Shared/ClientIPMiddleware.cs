namespace InfoBoard.Shared
{
    public class ClientIPMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientIPMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, BlazorAppContext appContext)
        {
            var clientIP = context.Connection.RemoteIpAddress?.ToString();
            appContext.CurrentUserIP = clientIP;

            await _next(context);
        }
    }
}
