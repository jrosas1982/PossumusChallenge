using Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace API.PossumusChallenge.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuth : Attribute, IAuthorizationFilter
    {
        private readonly ApplicationDBContext _db;
        public const string ApiKeyHeaderName = "ApiKey";

        public ApiKeyAuth(ApplicationDBContext db)
        {
            _db = db;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsApiKey = context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var PossibleApiKey);

            if (IsApiKey)
            {
                // Busco usuario por ApiKey en la base de datos
                var usuario = _db.Users.SingleOrDefault(x => x.ApiKey.Equals(PossibleApiKey));
                if (usuario != null)
                {
                    var Claims = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, $"{usuario.Name}")
                        });
                    context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(Claims));
                    return;
                }
            }
            context.Result = new UnauthorizedResult();
        }
    }
}
