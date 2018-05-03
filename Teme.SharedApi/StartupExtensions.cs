using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.OrgScopes;

namespace Teme.SharedApi
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddRscAuth(this IServiceCollection services, IConfiguration configuration, X509Certificate2 cert, IEnumerable<string> scopes)
        {
            services.AddIdentity<AuthUser, Role>()
                .AddDefaultTokenProviders();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["IdentityConfig:Issuer"],
                        ValidAudiences = scopes.ToArray(),
                        IssuerSigningKey = new X509SecurityKey(cert)
                    };
                });

            return services;
        }
    }
}
