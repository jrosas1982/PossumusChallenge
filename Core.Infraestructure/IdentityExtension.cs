using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Infraestructure
{
    //No usado en esta implementación
    public static  class IdentityExtension
    {
        public static string GetUsername(this IHttpContextAccessor httpContext)
        {
            if (httpContext == null)
                return null;

            var claim = ((ClaimsIdentity)httpContext.HttpContext.User.Identity).FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }
    }
}
