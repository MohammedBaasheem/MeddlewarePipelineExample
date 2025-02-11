using System.Diagnostics;

namespace meddleware_pipline.Meddlewares
{
    public class ProfilingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public ProfilingMiddleware(RequestDelegate next,ILogger<ProfilingMiddleware>logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task Invoke(HttpContext context)
        {
            var stopwash=new Stopwatch();
            stopwash.Start();
           await _next(context);
            stopwash.Stop();
            _logger.LogInformation($"Request '{context.Request.Path}' took '{stopwash.ElapsedMilliseconds}ms' to execute");
        }
    }
}
