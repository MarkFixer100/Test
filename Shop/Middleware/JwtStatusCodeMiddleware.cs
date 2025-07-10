using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;

namespace Shop.filters
{
    public class JwtStatusCodeMiddleware

    {
        private readonly RequestDelegate _next;


        public JwtStatusCodeMiddleware(RequestDelegate next)
        {
            _next = next;
           
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            var token = context.Request.Headers["authorization"]
                .FirstOrDefault()?.Split(" ").Last();

            if (IsTokenExpired(token))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    error = "token_expired",
                    message = "Token has expired",
                    timestamp = DateTime.UtcNow
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                return; 
            }
            if (string.IsNullOrEmpty(token))
            {
                await _next(context);
                
                return;
            }

            await _next(context);
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(token);
                return jwt.ValidTo < DateTime.UtcNow;
            }
            catch (Exception ex)
            {
               return false;
            }
        }
    }

}
