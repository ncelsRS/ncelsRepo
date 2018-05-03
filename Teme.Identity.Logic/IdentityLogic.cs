using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Teme.Shared.Data.Primitives.OrgScopes;
using Teme.Shared.Logic;

namespace Teme.Identity.Logic
{
    public class IdentityLogic : BaseLogic, IIdentityLogic
    {
        private readonly IConfiguration _config;
        private readonly X509Certificate2 _cert;

        public IdentityLogic(IConfiguration config, X509Certificate2 cert)
        {
            _config = config;
            _cert = cert;
        }


        public string GenerateOneTimeToken(int userId)
        {
            var secondsStr = _config["IdentityConfig:OneTimeExpirationSeconds"];
            if (!int.TryParse(secondsStr, out var seconds))
                seconds = (int)TimeSpan.FromMinutes(1).TotalSeconds;

            return GenerateToken(userId, seconds, new[] { OrganizationScopeEnum.Identity });
        }

        public string GenerateAccessToken(int userId, IEnumerable<string> audiences)
        {
            var secondsStr = _config["IdentityConfig:AccessExpirationSeconds"];
            if (!int.TryParse(secondsStr, out var seconds))
                seconds = (int)TimeSpan.FromMinutes(30).TotalSeconds;

            return GenerateToken(userId, seconds, audiences);
        }

        public string GenerateRefreshToken(int userId)
        {
            var secondsStr = _config["IdentityConfig:RefreshExpirationSeconds"];
            if (!int.TryParse(secondsStr, out var seconds))
                seconds = (int)TimeSpan.FromDays(30).TotalSeconds;

            return GenerateToken(userId, seconds, new[] { OrganizationScopeEnum.Identity });
        }



        private string GenerateToken(int userId, int seconds, IEnumerable<string> audiences)
        {
            var issuer = _config["IdentityConfig:Issuer"];
            var certPath = _config["IdentityConfig:CertPath"];
            var certPass = _config["IdentityConfig:CertPass"];

            var claims = audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)).ToList();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));

            var creds = new SigningCredentials(new X509SecurityKey(_cert), SecurityAlgorithms.RsaSha256);
            var expires = DateTime.Now.AddSeconds(seconds);

            var token = new JwtSecurityToken(
                issuer,
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
