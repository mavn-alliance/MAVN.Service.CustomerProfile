using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Falcon.Common.Encryption;
using JetBrains.Annotations;
using Lykke.Common.Api.Contract.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MAVN.Service.CustomerProfile.Middleware
{
    public class EncryptionKeyMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly IEnumerable<string> ExclusionSegments = new List<string> {"/api/isalive", "/api/encryptionkey"};
        
        private const string ErrorMessage = "Please set encryption key first";
        
        public EncryptionKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        public async Task InvokeAsync(HttpContext context, IAesSerializer serializer)
        {
            var path = context.Request.Path;
            
            if (path.HasValue && ExclusionSegments.Any(s => path.StartsWithSegments(s)))
            {
                await _next.Invoke(context);
            }
            else
            {
                if (!serializer.HasKey)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "application/json";
                    var json = ErrorResponse.Create(ErrorMessage).ToJson();
                    await context.Response.WriteAsync(json);

                    return;
                }

                await _next.Invoke(context);
            }
        }
    }

    public static class EncryptionKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseEncryptionKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EncryptionKeyMiddleware>();
        }
    }
}
