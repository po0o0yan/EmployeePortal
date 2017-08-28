using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Api.Middlewares
{
    public class ResponseHeadersMiddleware
    {
        //Access-Control-Allow-Origin
        private readonly RequestDelegate _next;

        public ResponseHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            await _next.Invoke(httpContext).ConfigureAwait(false);
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

        }


    }
    public static class ResponseHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseHeadersMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseHeadersMiddleware>();
        }
    }
}
