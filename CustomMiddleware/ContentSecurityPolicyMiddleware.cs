using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Brickwell.CustomMiddleware
{
  

    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate _next;

        public ContentSecurityPolicyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the Content-Security-Policy header is already present
            if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                // Define your Content Security Policy (CSP)
                string csp = "default-src 'self'; " + // Specify allowed sources for default content
                             "script-src 'self' 'unsafe-inline'; " + // Allow scripts only from the same origin and inline scripts
                             "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " + // Allow styles only from the same origin, inline styles, and fonts.googleapis.com
                             "img-src 'self' data: https://m.media-amazon.com https://www.lego.com https://images.brickset.com https://www.brickeconomy.com; " + // Allow images only from the same origin, data URIs, and specified external sources
                             "font-src 'self' https://fonts.gstatic.com;"; // Allow fonts only from the same origin and fonts.gstatic.com

                // Add Content-Security-Policy header
                context.Response.Headers.Add("Content-Security-Policy", csp);
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

}
