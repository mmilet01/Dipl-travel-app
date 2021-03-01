using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class AddLastActivityTimeStampMiddleware
    {
        private readonly RequestDelegate _next;
        public AddLastActivityTimeStampMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            mmiletaContext _context
            )
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var user = _context.Users.FirstOrDefault(x => x.Email == userEmail);
                user.LastActivityTimeStamp = DateTime.Now;
                if(!user.IsActive)
                {
              //      user.IsActive = true;
                    // notify via singalR and notification hub
                }
                await _context.SaveChangesAsync();
            }
            await _next(context);
        }
    }
}
