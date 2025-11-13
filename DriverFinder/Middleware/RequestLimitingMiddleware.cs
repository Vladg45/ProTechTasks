using DriverFinder.Services;

namespace DriverFinder.Middleware
{
    public class RequestLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestLimiter _requestLimiter;
        private readonly ILogger<RequestLimitingMiddleware> _logger;

        public RequestLimitingMiddleware(
            RequestDelegate next,
            IRequestLimiter requestLimiter,
            ILogger<RequestLimitingMiddleware> logger)
        {
            _next = next;
            _requestLimiter = requestLimiter;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Пропускаем Swagger UI без ограничений
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!_requestLimiter.TryAcquire())
            {
                _logger.LogWarning($"HTTP 503: Лимит параллельных запросов достигнут для {context.Request.Method} {context.Request.Path}");
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Service Unavailable: Превышен лимит параллельных запросов");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                _requestLimiter.Release();
            }
        }
    }
}
