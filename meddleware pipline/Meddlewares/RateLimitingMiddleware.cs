﻿namespace meddleware_pipline.Meddlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static int _counter = 0;
        private static DateTime _lastRequestDate = DateTime.Now;
        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            _counter++;
            if (DateTime.Now.Subtract(_lastRequestDate).Seconds > 10) 
            { 
                _counter =1;
                _lastRequestDate = DateTime.Now;
                await _next(context);
            
            }
            else
            {
                if (_counter>5)
                {
                    _lastRequestDate = DateTime.Now;
                    await context.Response.WriteAsync("Rate Limit Exceeded.");
                }
                else
                {
                    _lastRequestDate = DateTime.Now;
                    await _next(context);
                }
            }

        }
    }
}
